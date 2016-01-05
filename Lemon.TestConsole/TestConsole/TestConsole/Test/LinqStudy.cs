using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TestConsole
{
    public class LinqStudy
    {
        public static void DoTest()
        {
            // 匿名类型
            var worker = new { FirstName = "Vincent", LastName = "Ke", Level = 2 };
            Console.WriteLine("name: {0} {1}, level:{2}", worker.FirstName, worker.LastName, worker.Level);
            Console.ReadKey();

            // 匿名类型的 相等语义
            var worker1 = new { FirstName = "Harry", LastName = "Folwer", Level = 2 };
            var worker2 = new { FirstName = "Harry", LastName = "Folwer", Level = 2 };

            Console.WriteLine("Equals: {0},  '==': {1}, IsSameType: {2}",
                                worker1.Equals(worker2),
                                worker1 == worker2,
                                worker1.GetType().Name == worker2.GetType().Name);
            Console.ReadKey();
        }

        public static void DoTest1()
        {
            string testStr = "The quick brown fox jumped over the lazy dog.";

            // 扩展方法
            Console.WriteLine("word count: {0}", testStr.WordCount());
            Console.WriteLine("word count + 3: {0}", testStr.WordCountPlus(3));
            Console.ReadKey();
        }

        public static void DoTest2()
        {
            // 初始化语法
            Point point = new Point { X = 1, Y = 2 };

            // 集合初始化
            int[] numbers = { 1, 2, 3, 4, 5 };
            ArrayList list = new ArrayList { 1, 2, 3, 4, 5 };
        }

        public static void DoTest3()
        {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            // LINQ方法语法
            IEnumerable<string> query = names.Where(s => s.Contains("a"))
                                             .OrderBy(s => s.Length)
                                             .Select(s => s.ToUpper());
            foreach (string s in query)
            {
                Console.Write(s + " ");
            }
            Console.ReadKey();
        }

        public static void DoTest4()
        {
            //List<string[]> list = new List<string[]>();
            //list.Add(new string[] { "aa", "AA" });
            //list.Add(new string[] { "bb", "BB" });
            //list.Add(new string[] { "cc", "CC" });
            //list.Add(new string[] { "dd", "DD" });

            string[] list = { "aa", "bb", "cc", "dd" };
            string[] list2 = { "AA", "BB", "CC", "DD" };

            bool[] arrb = { true, false, true, false };

            var sm = 
                list.Zip(list2, (l,r)=> new string[]{ l, r})
                    .Where((s, i) => arrb[i])
                    .SelectMany(s => s);
            Console.WriteLine(string.Join(",", sm));
            Console.ReadKey();
        }


    }

    // 用来支撑扩展方法的静态类
    public static class LemonExtension
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', ',', '?' }, StringSplitOptions.RemoveEmptyEntries)
                      .Length; 
        }

        public static int WordCountPlus(this string str, int n)
        {
            return str.WordCount() + n;
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
