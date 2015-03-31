using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Enum;
using System.Data.Common;
using TestConsole.Test.SqlWhere.Part;
using System.Data;

namespace TestConsole.Test.SqlWhere
{
    public partial class SqlWhere : ExpressionBase, IWhere
    {
        private readonly DbTypeEnum dbType = DbTypeEnum.Oracle;
        private string sqlString = string.Empty;
        private IDbDataParameter[] parems = null;
        private string paramPrefix = null;

        #region constructors
        public SqlWhere(DbTypeEnum dbType)
            : this(dbType, null)
        {
        }

        public SqlWhere(DbTypeEnum dbType, object param)
        {
            this.dbType = dbType;
            ObjectParam = param;
        }
        #endregion

        #region IWhere
        public object ObjectParam { get; set; }

        public IDbDataParameter[] Params
        {
            get
            {
                if (!hasProcessed)
                {
                    Process();
                }
                return parems;
            }
        }

        public string ToSqlString()
        {
            if (!hasProcessed)
            {
                Process();
            }
            return sqlString;
        }

        #endregion

        private string ParamPrefix
        {
            get
            {
                if (string.IsNullOrEmpty(paramPrefix))
                {
                    switch (dbType)
                    {
                        case DbTypeEnum.Oracle:
                            {
                                paramPrefix = ":";
                                break;
                            }
                    }
                }
                return paramPrefix;
            }
        }
    }
}
