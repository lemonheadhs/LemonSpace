using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestConsole.Test
{
    public class DelegateStudy
    {
        //protected Action testDele;
        public static void DoTest()
        {
            Action testDele;
            //Console.WriteLine(testDele == null);//报编译错误：使用了未赋值的局部变量
            //testDele += Greeting;//报编译错误：使用了未赋值的局部变量
            testDele = Greeting;
            Console.WriteLine(testDele == null);//编译通过  False
            testDele += SayGoodbye;

            testDele();

            testDele -= SayGoodbye;
            testDele -= Greeting;

            //testDele.Equals(null);//运行时异常：未处理NullReferenceException
            Console.WriteLine(testDele == null);// True

            Console.ReadKey();
            
        }

        public static void DoTest1()
        {
            Action testDele;
            testClass tc = new testClass();
            otherTestClass otc = new otherTestClass { innerWords = "9876" };

            testDele = tc.Talk;//实例方法
            testDele += otc.Talk;//扩展方法，使用实例方法调用的语法，
            testDele += Greeting;//静态方法

            //           _target、           _methodPtr、_methodPtrAux
            //  tc.Talk: testClass实例       2346040     0
            // otc.Talk: otherTestClass实例  2346112     0              -- 说明这里委托把扩展方法当做实例方法对待了
            // Greeting: 委托本身            7803012     2345000
            //
            
            testDele();

            Console.ReadKey();
        }

        public static void DoTest2()
        {
            //1.
            MethodInfo m1 = typeof(testClass)
                .GetMethod("Talk",
                            BindingFlags.Public
                          | BindingFlags.Instance);
            testClass tc = new testClass();
            Action closedInstanceDlgt = tc.Talk;
            closedInstanceDlgt();//普通实例方法委托
            //等效：closedInstanceDlgt = (Action)Delegate.CreateDelegate(typeof(Action), tc, m1);

            //2.
            Action<testClass> openInstanceDlgt = (Action<testClass>)Delegate.CreateDelegate(typeof(Action<testClass>), null, m1);
            openInstanceDlgt(tc);//开放式实例委托

            //3.
            MethodInfo m2 = typeof(testClass)
                .GetMethod("TalkStatic", 
                            BindingFlags.Public 
                          | BindingFlags.Static);
            Action<string> openStaticDlgt = testClass.TalkStatic;
            openStaticDlgt("abcde");//普通静态方法委托
            //等效：openStaticDlgt = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), null, m2);

            //4.
            Action closedStaticDlgt = (Action)Delegate.CreateDelegate(typeof(Action), "fghij", m2);
            closedStaticDlgt();//封闭式静态委托

            Console.ReadKey();//感觉 开放式实例委托 和 封闭式静态委托，没有什么很实际的用途？
        }




        internal class testClass
        {
            string words = "1234";
            public void Talk()
            {
                Console.WriteLine(words);
            }

            public static void TalkStatic(string words)
            {
                Console.WriteLine(words);
            }
        }

        #region 私有方法

        
        private static void Greeting()
        {
            Console.WriteLine("Hi,lemon!");
        }
        private static void SayGoodbye()
        {
            Console.WriteLine("ByeBye,lemon!");
        }

        #endregion
    }

    internal static class testClassHelper
    {
        public static void Talk(this otherTestClass obj)
        {
            Console.WriteLine(obj.innerWords);
        }
    }

    internal class otherTestClass
    {
        public string innerWords { get; set; }
    }
}
