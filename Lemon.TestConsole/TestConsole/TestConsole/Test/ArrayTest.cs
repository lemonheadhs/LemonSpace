using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class ArrayTest
    {
        public static void DoTest()
        {
            int[] intArr = { 2, 5, 6, 3, 7, 1, 4 };
            Array.Sort(intArr);

            Console.ReadKey();
        }
    }
}
