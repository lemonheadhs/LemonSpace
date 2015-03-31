using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class AlgorithmsStudy
    {
        // 假设参与排序的是整型一维数组

        public static void DoTest()
        {
            int[] aToTest = { 2, 6, 4, 1, 3, 5 };

            LemonQuickSort(aToTest);

            Console.ReadKey();
        }

        private static void LemonQuickSort(int[] intArr)
        {
            if (intArr.Length > 0)
            {
                LemonQuickSort(0, intArr.Length - 1, intArr);
            }
        }

        // 快速排序
        private static void LemonQuickSort(int leftIndex, int rightIndex, int[] intArr)
        {
            do
            {
                int i = leftIndex;
                int j = rightIndex;

                int m = GetMedian(leftIndex, rightIndex);

                SwapIfGreaterThan(i, m, intArr);
                SwapIfGreaterThan(i, j, intArr);
                SwapIfGreaterThan(m, j, intArr);

                int medianElemt = intArr[m];

                do
                {
                    while (intArr[i] < medianElemt) i++;
                    while (medianElemt < intArr[j]) j--;

                    if (i > j) break;
                    if (i < j)
                    {
                        Swap(i, j, intArr);
                    }
                    i++;
                    j--;
                } while (i <= j);

                if (j - leftIndex >= rightIndex - i)
                {
                    if (leftIndex < j)
                    {
                        LemonQuickSort(leftIndex, j, intArr);
                    }
                    leftIndex = i;
                }
                else
                {
                    if (i < rightIndex)
                    {
                        LemonQuickSort(i, rightIndex, intArr);
                    }
                    rightIndex = j;
                }// 这样做递归，好像调用深度会减小一点，减少递归次数，不知道里面有什么玄机？？

            } while (leftIndex < rightIndex);
        }

        private static int GetMedian(int leftIndex, int rightIndex)
        {
            return leftIndex + ((rightIndex - leftIndex) >> 1);
        }

        private static void Swap(int i, int j, int[] intArr)
        {
            int tempInt = intArr[i];
            intArr[i] = intArr[j];
            intArr[j] = tempInt;
        }
        private static void SwapIfGreaterThan(int i, int j, int[] intArr)
        {
            if (intArr[i] > intArr[j])
            {
                int tempInt = intArr[i];
                intArr[i] = intArr[j];
                intArr[j] = tempInt;
            }
        }
    }
}
