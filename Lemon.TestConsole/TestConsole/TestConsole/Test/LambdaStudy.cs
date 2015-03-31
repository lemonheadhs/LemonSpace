using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    public class LambdaStudy
    {
        #region DoTest
        public static void DoTest()
        {
            int result = 0;
            result = SumOfSquares(3, 4);

            Console.WriteLine("3*3 + 4*4 = {0}", result);
            Console.ReadKey();
        }

        private static int IntegerOperation(int left, int right, Func<int, int, int> operatr)
        {
            return operatr(left, right);
        }

        // 这个例子实在是有点脱了裤子放屁的感觉。。
        private static int SumOfSquares(int left, int right)
        {
            return IntegerOperation(left, right,
                    (l, r) =>
                    { return l * l + r * r; });
        }
        #endregion

        #region DoTest1
        public static void DoTest1()
        {
            string[] arr = new string[]{"张三", "李四", "王五", "lemon"};
            List<string> list = new List<string>(arr);

            string result = list.Find(str => str.Length >= 5 );

            Console.WriteLine("张三, 李四, 王五, lemon");
            Console.WriteLine("长度大于等于5的是： {0}", result);
            Console.ReadKey();
        }

        #endregion

    }
}
