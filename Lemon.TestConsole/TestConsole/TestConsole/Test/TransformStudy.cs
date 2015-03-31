using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    public class TransformStudy
    {
        public static void DoTest()
        {
            object[] unformated = { "Hello!", "lemon" };
            object source = unformated;
            TTarget target = (TTarget)source;
        }
    }

    public class TTarget
    {
        private string field_1;
        private string field_2;
    }
}
