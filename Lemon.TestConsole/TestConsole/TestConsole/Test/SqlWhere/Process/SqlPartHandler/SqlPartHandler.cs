using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Part;

namespace TestConsole.Test.SqlWhere.Process.SqlPartHandler
{
    // 返回是否进行了处理，如果不符合条件就不会处理
    public delegate bool SqlPartHandler(SqlContext ctx, SqlPart part, object paramObject);
}
