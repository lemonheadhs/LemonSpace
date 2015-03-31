using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class YieldStudy
    {
        public string name {get;set;}

        public static void DoTest() 
        { 
            foreach(var i in new YieldTest())
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }

        public static void DoTest1()
        {
            YieldTest model = new YieldTest();
            foreach (var item in model.GetInstances())
            {
                Console.WriteLine(item.name);
            }
            Console.ReadKey();
        }
    }

    internal class YieldTest
    {
        public IEnumerator<int> GetEnumerator()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
            yield return 5;
        }

        public IEnumerable<YieldStudy> GetInstances()
        {
            YieldStudy ins = new YieldStudy();
            ins.name = "lemon";
            yield return ins;
            ins.name = "Ford";
            yield return ins;
        }
    }
}
