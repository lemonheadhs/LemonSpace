using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestConsole.Test
{
    public static class ParallelProgramStudy
    {
        public static void DoTest()
        { }
    }

    public static class MultiThreadStudy
    {
        // 认识线程
        public static void DoTest()
        {
            Thread thread = Thread.CurrentThread;
            thread.Name = "Main Thread";

            Console.Write(string.Format(@"Thread ID:{0}
    Current AppDomainId:{1}
    Current ContextId:{2}
    Thread Name:{3}
    Thread State:{4}
    Thread Priority:{5}", thread.ManagedThreadId, Thread.GetDomainID(), Thread.CurrentContext.ContextID, thread.Name, thread.ThreadState, thread.Priority));
            Console.WriteLine();
            Console.ReadKey();
        }

        // ThreadStart委托
        public static void DoTest1()
        {
            Console.WriteLine(string.Format("Main threadId:{0}", Thread.CurrentThread.ManagedThreadId));
            Thread thread = new Thread(new ThreadStart(ShowMessage));
            thread.Start();
                // Start 方法立即返回，经常是在实际启动新线程之前
            // 线程一旦启动，就不必保留对 Thread 对象的引用。 该线程会继续执行，直到线程过程结束为止

            Console.WriteLine("do something!......................");
            Console.WriteLine("Main thread working is complete!");
        }

        // ThreadPool线程池
        public static void DoTest2()
        {
            //ThreadPool.SetMaxThreads(1000, 1000);
            Console.WriteLine("Main thread start..");

            ThreadPool.QueueUserWorkItem(new WaitCallback(ShowMessage));

            Console.WriteLine("Return to Main thread..");
            Console.ReadKey();
        }

        // 委托的BeginInvoke/EndInvoke方法   --内部使用的就是CLR线程池中的工作者线程
        public static void DoTest3()
        {
            Console.WriteLine("Main thread start..");
            MyDelegate asynFunc = new MyDelegate(ShowMessage);
            IAsyncResult asynRes = asynFunc.BeginInvoke(null/*回调方法委托*/, null/*回调方法参数*/);
            Console.WriteLine("Main thread do something, after BeginInvoke..");
            Thread.Sleep(600);
            Console.WriteLine("Main thread do something2..");
            Thread.Sleep(600);
            Console.WriteLine("Before EndInvoke..");
            asynFunc.EndInvoke(asynRes);
            Console.WriteLine("After EndInvoke..");
            Console.ReadKey();
        }

        // “王富贵问题”
        public static void DoTest4()
        {
            // 例子来自 http://www.cnblogs.com/xhu218/archive/2015/02/05/4274584.html#!comments
            //list里存放10个数字
            
            List<int> list = Enumerable.Range(0, 10).ToList();  //下面的方式等效，但显然这个更好！
            //List<int> list = new List<int>(10);
            //for (int i = 0; i < 10; i++)
            //{
            //    list.Add(i);
            //}

            //10个数字,分成10组,其实每组就一个元素,每组的元素是不相同的
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < 10; i++)
            {
                int k = i % 10;
                if (dict.ContainsKey(k))
                {
                    dict[k].Add(i);
                }
                else
                {
                    dict[k] = new List<int>();
                    dict[k].Add(i);
                }
            }

            //foreach (var keyValue in dict)
            //{
            //    Thread thread = new Thread(Display);
            //    thread.Start(keyValue.Value);
            //}
            Console.WriteLine("--------------------------------------------");//实际在多线程的情况下，这条线的输出位置，未必就能标识上下两处代码的输出分隔位置；多线程真是很有趣
            //foreach (var keyValue in dict)//错误
            //{
            //    Thread thread = new Thread(delegate()
            //    {
            //        foreach (var item in keyValue.Value)
            //        {
            //            Console.WriteLine(item.ToString());
            //        }
            //    });
            //    thread.Start();
            //}
            //foreach (var keyValue in dict)
            //{
            //    Thread thread = new Thread(() =>
            //    {
            //        foreach (var item in keyValue.Value)
            //        {
            //            Console.WriteLine(DateTime.Now.Ticks + " " + Thread.CurrentThread.ManagedThreadId + " " + item.ToString());
            //        }
            //    });
            //    thread.Start();
            //}
            //foreach (var keyValue in dict)//正确
            //{
            //    Thread thread = new Thread(delegate(object o)
            //    {
            //        List<int> li = o as List<int>;
            //        foreach (var item in li)
            //        {
            //            Console.WriteLine(item.ToString());
            //        }
            //    });
            //    thread.Start(keyValue.Value);
            //}

            //using (Dictionary<int, List<int>>.Enumerator enumerator = dict.GetEnumerator())//错误
            //{
            //    KeyValuePair<int, List<int>> keyValue;
            //    while (enumerator.MoveNext())
            //    {
            //        lock (enumerator.Current.Value) //enumerator.Current.Value 这个值没有锁住 在第2次循环 而foreach没结束的时候会修改掉的
            //        {
            //            keyValue = enumerator.Current;
            //            Thread thread = new Thread(delegate()
            //            {
            //                foreach (var item in keyValue.Value)
            //                {
            //                    Console.WriteLine(item.ToString());
            //                }
            //            });
            //            thread.Start();
            //        }
            //    }
            //}

            //using (Dictionary<int, List<int>>.Enumerator enumerator = dict.GetEnumerator())//正确
            //{
            //    while (enumerator.MoveNext())
            //    {
            //        KeyValuePair<int, List<int>> keyValue = enumerator.Current;
            //        Thread thread = new Thread(delegate()
            //        {
            //            foreach (var item in keyValue.Value)
            //            {
            //                Console.WriteLine(item.ToString());
            //            }
            //        }
            //        );
            //        thread.Start();
            //    }
            //}

            //foreach (var keyValue in dict)//正确 ,与上面那个等价 --这也说明了，foreach的等价形式，keyValue是定义在while循环之外的
            //{
            //    var v = keyValue.Value;//去掉这个变量v，直接使用keyValue就不对了
            //    Thread thread = new Thread(delegate()
            //    {
            //        foreach (var item in v)
            //        {
            //            Console.WriteLine(item.ToString());
            //        }
            //    });
            //    thread.Start();
            //}

            Console.ReadKey();
        }

        public static void Display(object o)
        {
            List<int> list = o as List<int>;
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }

        delegate void MyDelegate();
        private static void ShowMessage()
        {
            Console.WriteLine(string.Format("Async threadId:{0}", Thread.CurrentThread.ManagedThreadId));

            for (int n = 0; n < 10; n++)
            {
                Thread.Sleep(300);
                Console.WriteLine("The number is:" + n.ToString());
            }
        }
        private static void ShowMessage(object argOb)
        {
            ShowMessage();
        }

        public static void DoTest5()
        {
            int n = 0;
            for (object o2 = new object(); n < 20; n++)
            {
                object o = new object();
                int n1 = n;

                //1.  不可以重现“王富贵问题”   -- ？？
                //ThreadPool.QueueUserWorkItem((e) => { Console.WriteLine(e.GetHashCode()); }, n);
                //ThreadPool.QueueUserWorkItem((e) => { Console.WriteLine(n); });

                //2.  可以重现“王富贵问题”
                Thread thread = new Thread(() =>
                {
                    //Console.WriteLine(o.GetHashCode());
                    Console.WriteLine(n);// 错误
                    //Console.WriteLine(n1);// 正确         -- 这两个差异怎么解释呢？ 闭包的实现原理
                });
                thread.Start();
            }
            Console.ReadKey();
        }
    }
}
