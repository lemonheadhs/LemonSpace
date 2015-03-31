using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Enum;
using TestConsole.Test.SqlWhere.Process.SqlPartHandler;

namespace TestConsole.Test.SqlWhere
{
    public partial class SqlWhere : ExpressionBase, IWhere
    {
        protected static Dictionary<PartTypeEnum, SqlPartHandler> handlers = new Dictionary<PartTypeEnum, SqlPartHandler>();

        static SqlWhere()
        {
            Register(PartTypeEnum.IsNotNull, ProcessIsNotNull);
        }

        private static void Register(PartTypeEnum ptype, SqlPartHandler handler)
        {
            handlers.Add(ptype, handler);
        }

        private SqlPartHandler GetHandler(PartTypeEnum ptype)
        {
            SqlPartHandler handler = null;
            if(SqlWhere.handlers.TryGetValue(ptype,out handler))
            {
                return handler;
            }
            return null; 
        }
    }
}
