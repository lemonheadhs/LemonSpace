using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace TestConsole.Test.SqlWhere
{
    public interface IWhere : IExpression
    {
        object ObjectParam { get; set; }

        string ToSqlString();

        IDbDataParameter[] Params { get; }
    }
}
