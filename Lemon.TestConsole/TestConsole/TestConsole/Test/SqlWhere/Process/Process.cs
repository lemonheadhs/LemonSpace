using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Part;
using TestConsole.Test.SqlWhere.Process.SqlPartHandler;
using TestConsole.Test.SqlWhere.Common;
using System.Data;

namespace TestConsole.Test.SqlWhere
{
    public partial class SqlWhere : ExpressionBase, IWhere
    {
        // 与 parems 和 sqlString的延迟加载有关
        private bool hasProcessed = false;


        /// <summary>
        /// 处理传入的参数，形成动态SQL和参数列表
        /// </summary>
        private void Process()
        {
            SqlContext ctx = new SqlContext(dbType);
            Process(ctx, Children, ctx.buffer);

            parems = ctx.list.ToArray();
            sqlString = ctx.buffer.ToString();
            hasProcessed = true;
        }

        /// <summary>
        /// 递归地处理内部的SqlPart
        /// </summary>
        /// <param name="ctx">context对象，用来保存处理结果</param>
        /// <param name="Children">当前节点的子节点</param>
        /// <returns></returns>
        private bool Process(SqlContext ctx, ExpressionCollection Children, StringBuilder buff)
        {
            bool isInclude = false;// 用来标记本层子节点有没有经过处理

            foreach (IExpressionChild child in Children)
            {
                if (child is SqlPart)
                {
                    SqlPart part = (SqlPart)child;
                    isInclude = isInclude || ProcessSqlPart(ctx, part, ObjectParam, buff);// 进行了处理需要进行标记
                    continue;
                }
                else if (child is ExpressionBase)
                {
                    ExpressionBase sub = (ExpressionBase)child;

                    StringBuilder lowerBuf = new StringBuilder();
                    bool subInclude = Process(ctx, sub.Children, lowerBuf);

                    if (subInclude)// 递归中进行过处理，需要在处理结果前加 prepend
                    {
                        if (isInclude)// 之前有兄弟节点经过了处理，就需要加 prepend
                        {
                            lowerBuf.Insert(0, " " + sub.Prepend + " ");
                        }
                        ctx.buffer.Append(lowerBuf.ToString());
                    }
                    isInclude = isInclude || subInclude;
                }
            }
            return isInclude;
        }

        /// <summary>
        /// 处理 SqlPart，取出其中的SQL语句和参数
        /// </summary>
        /// <param name="ctx">context对象</param>
        /// <param name="part">待处理的 SqlPart</param>
        /// <param name="paramObject">参数对象</param>
        /// <param name="buff">用来保存SQL的StringBuilder</param>
        /// <returns>是否有进行处理</returns>
        private bool ProcessSqlPart(SqlContext ctx, SqlPart part, object paramObject, StringBuilder buff)
        {
            SqlPartHandler handler = GetHandler(part.pType);
            bool matchLimits = false;
            if (handler != null)
            {
                matchLimits = handler(ctx, part, ObjectParam);// 进行了处理需要进行标记

                if (matchLimits)
                {
                    // 解析SQL字符串，将参数取出来，拼接新的SQL
                    Tokenizer tokenizer = new Tokenizer(part.SqlStr, "#", true /* returnDeli */);
                    IEnumerator<string> enumrator = tokenizer.GetEnumerator();
                    string lastToken = null;
                    string token = null;

                    while (enumrator.MoveNext())
                    {
                        token = enumrator.Current;

                        if (tokenizer.delimeter.Equals(token))// 取到分隔符
                        {
                            // 这里什么都不需要做
                        }
                        else
                        {
                            if (lastToken == null)// 取到SQL部分
                            {
                                buff.Append(token);
                            }
                            else// 取到参数部分
                            {
                                string paramName = ParamPrefix + "param" + ctx.list.Count.ToString();
                                IDbDataParameter dbParam = null;
                                dbParam.ParameterName = paramName;
                                dbParam.DbType = DbType.String;
                                dbParam.Value = null;

                                buff.Append(paramName);
                                ctx.list.Add(dbParam);

                                // 取出参数后面的分隔符
                                enumrator.MoveNext();
                                token = enumrator.Current;
                                if (tokenizer.delimeter.Equals(token))
                                {
                                    lastToken = null;
                                    continue;
                                }
                                else
                                {
                                    throw new Exception("SqlWhere 配置的SQL格式不正确");
                                } 
                            }
                        }
                        lastToken = token;
                    }
                }
                return matchLimits;
            }
            return false;
        }
    }
}
