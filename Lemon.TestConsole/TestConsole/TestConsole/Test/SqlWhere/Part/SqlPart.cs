using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Enum;

namespace TestConsole.Test.SqlWhere.Part
{
    public class SqlPart : IPart, IExpressionChild
    {
        public string SqlStr { get; set; }
        public string Prepend { get; set; }
        public PartTypeEnum pType { get; set; }

        public SqlPart(string sqlPart)
            : this(sqlPart, string.Empty, PartTypeEnum.IsNotNull)
        {
        }

        public SqlPart(string sql, string prepend, PartTypeEnum ptype)
        {
            SqlStr = sql;
            Prepend = prepend;
            pType = ptype;
        }
    }
}
