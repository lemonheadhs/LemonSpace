using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Part;
using TestConsole.Test.SqlWhere.Enum;

namespace TestConsole.Test.SqlWhere
{
    public interface IExpression : IExpressionChild, IExpressionParent
    {
        string Prepend { get; set; }

        /// <summary>
        /// 根据传入的SQL字符串创建谓词 And的 SqlPart对象
        /// </summary>
        /// <param name="partString">sql短句</param>
        /// <returns>代表输入短句的 SqlPart对象</returns>
        IExpression And_(string partString);

        /// <summary>
        /// 根据传入的SQL字符串创建谓词 Or的 SqlPart对象
        /// </summary>
        /// <param name="partString">sql短句</param>
        /// <returns>代表输入短句的 SqlPart对象</returns>
        IExpression Or_(string partString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partString">sql短句</param>
        /// <returns>代表输入短句的 SqlPart对象</returns>
        IExpression _(string partString);

        /// <summary>
        /// 根据传入的SQL字符串创建谓词 And的 SqlPart对象
        /// </summary>
        /// <param name="partString">sql短句</param>
        /// <param name="pType">PartTypeEnum类型枚举，指示应该如何处理短句</param>
        /// <returns>代表输入短句的 SqlPart对象</returns>
        IExpression And_(string partString, PartTypeEnum pType);

        /// <summary>
        /// 根据传入的SQL字符串创建谓词 And的 SqlPart对象
        /// </summary>
        /// <param name="partString">sql短句</param>
        /// <param name="pType">PartTypeEnum类型枚举，指示应该如何处理短句</param>
        /// <returns>代表输入短句的 SqlPart对象</returns>
        IExpression Or_(string partString, PartTypeEnum pType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partString">sql短句</param>
        /// <param name="pType">PartTypeEnum类型枚举，指示应该如何处理短句</param>
        /// <returns>代表输入短句的 SqlPart对象</returns>
        IExpression _(string partString, PartTypeEnum pType);

        IExpression And_(IExpression subExp);
        IExpression Or_(IExpression subExp);
        IExpression _(IExpression subExp);
    }

    public interface IExpressionChild
    {
    }

    public interface IExpressionParent
    {
        ExpressionCollection Children { get; set; }
    }
}
