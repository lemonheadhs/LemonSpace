using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class DictionaryStudy
    {
        public static void DoTest()
        {
            Console.WriteLine("DictionaryStudy Start:");

            string[] ar1 = { "lemon", "john", "tom" };
            string[] ar2 = { "jopherf", "titan", "john", "lemon" };
            string[] ar3 = { "titan", "bickman", "lemon" };

            string[][] tt = {};//{ ar1, ar2, ar3 };

            string[] finalSet = GetMergedSet(tt);
        }

        private static string[] GetMergedSet(string[][] arrs)
        {
            Dictionary<string, object> targetDic = new Dictionary<string, object>();

            foreach (var ar in arrs)
            {
                SetInNoDulplicate(targetDic, ar);
            }

            string[] finalSet = new string[targetDic.Count];
            targetDic.Keys.CopyTo(finalSet, 0);

            return finalSet;
        }

        private static void SetInNoDulplicate(Dictionary<string, object> targetDic, string[] arr)
        {
            foreach (string name in arr)
            {
                if (!targetDic.ContainsKey(name))
                {
                    targetDic.Add(name, null);
                }
            }
        }
    }
}
