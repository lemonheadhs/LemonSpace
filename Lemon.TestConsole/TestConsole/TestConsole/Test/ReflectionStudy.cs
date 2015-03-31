using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestConsole.Test
{
    public class ReflectionStudy
    {
        public static void DoTest()
        {
            Console.WriteLine("ReflectionStudy Start.");

            Type type = typeof(string);

            MethodInfo method = type.GetMethod("Substring",
                new Type[] { typeof(int), typeof(int) });

            object result = method.Invoke("Hello, lemon!", new object[] { 7, 5 });

            Console.WriteLine("Result: {0}", result);
        }
    }
}
