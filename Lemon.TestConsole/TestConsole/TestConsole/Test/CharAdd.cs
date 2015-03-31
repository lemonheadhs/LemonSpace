using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class CharAdd
    {
        public static void DoTest()
        {
            char c = 'a';
            string cStr = "a";
            var code = char.ConvertToUtf32(cStr, 0);

            var res = char.ConvertFromUtf32(code + 1);

            Console.ReadKey();

        }
    }
}
