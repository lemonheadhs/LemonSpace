using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TestConsole.Test.SqlWhere.Enum;

namespace TestConsole.Test.SqlWhere
{
    public class SqlContext
    {
        private readonly DbTypeEnum dbType;

        public StringBuilder buffer = new StringBuilder();
        public List<IDbDataParameter> list = new List<IDbDataParameter>();

        public SqlContext(DbTypeEnum type)
        {
            this.dbType = type;
        }
    }
}
