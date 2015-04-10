using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class HashSetStudy
    {
        public static void DoTest()
        {
            int[] duplicateArr = new int[]{ 1,1,2,2,3,3,4,4,4 };
            HashSet<int> hs = new HashSet<int>(duplicateArr);//创建hs时会自动去重
            foreach (var n in hs)
            {
                Console.WriteLine(n);
            }

            hs.Add(4);//会忽略加入的重复元素
            Console.WriteLine("------------------------");
            foreach (var n in hs)
            {
                Console.WriteLine(n);
            }

            Console.ReadKey();
        }
    }
}
