using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Part;

namespace TestConsole.Test.SqlWhere
{
    public partial class SqlWhere : ExpressionBase, IWhere
    {
        private static bool ProcessIsNotNull(SqlContext ctx, SqlPart part, object paramObject)
        {
            return false;
        }
    }
}
