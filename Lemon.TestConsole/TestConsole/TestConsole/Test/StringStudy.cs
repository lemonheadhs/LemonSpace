using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class StringStudy
    {
        public static void DoTest()
        {
            string emptStr = string.Empty;

            string result = emptStr.TrimEnd(',');
        }
        public static void DoTest1() 
        {
            string _test = " or 123";
            //_test.TrimStart(' or ');

            string[] arrT = { " 1=1 ", " 2= 2 " };
            _test = string.Join(" or ", arrT);

            Console.ReadKey();
        }

        public static void DoTest2()
        {
            string _test = "~";
            //_test.TrimStart(' or ');
            string[] arr = _test.Split('~');

            Console.ReadKey();
        }

    }
}
