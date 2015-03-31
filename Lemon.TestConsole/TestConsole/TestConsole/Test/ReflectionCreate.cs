using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using TestConsole.FromJeffreyZhao.CodeTimer;

namespace TestConsole.Test
{
    public class ReflectionCreate
    {
        private delegate object Create();

        private static Create _create = null;
        private static Func<object> _create1 = null;

        // 各种方式创建对象的性能比较
        public static void DoTest()
        {
            Console.WriteLine("ReflectionCreate Start.");

            CodeTimer.Initialize();
            CodeTimer.Time("DirectCreate", 1,
                () =>
                {
                    for (int n = 0; n < 1000000; n++)
                    {
                        new Department();
                    }
                }
            );
            CodeTimer.Time("ReflectionCreate", 1,
                () =>
                {
                    for (int n = 0; n < 1000000; n++)
                    {
                        ReflectionCreateObject();
                    }
                }
            );
            CodeTimer.Time("EmitCreate", 1,
                () =>
                {
                    for (int n = 0; n < 1000000; n++)
                    {
                        EmitCreateObject();
                    }
                }
            );
            CodeTimer.Time("LambdaCreate", 1,
                () =>
                {
                    for (int n = 0; n < 1000000; n++)
                    {
                        LambdaCreateObject();
                    }
                }
            );
            Console.ReadKey();
        }

        // 使用传统反射的方法创建对象
        private static object ReflectionCreateObject()
        {
            object o = null;
            Type targetType = typeof(Department);
            int choice = 0;

            switch (choice)// 传统反射的3种方法       --  .netframework version 1.0以上支持
            {
                case 0:// Activator
                    {
                        o = Activator.CreateInstance(targetType);
                        break;
                    }
                case 1:// ConstructorInfo
                    {
                        ConstructorInfo ctor = targetType.GetConstructor(new Type[] { });
                        o = ctor.Invoke(null);
                        break;
                    }
                case 2:// Assembly
                    {
                        Assembly asm = Assembly.GetAssembly(typeof(Department));
                        o = asm.CreateInstance("TestConsole.Test.Department");
                        break;
                    }
            }
            return o;
        }

        // 使用Emit方法创建对象        --  .netframework version 2.0以上支持
        private static object EmitCreateObject()
        {
            if (_create == null)
            {
                Type targetType = typeof(Department);
                ConstructorInfo ctor = targetType.GetConstructor(new Type[] { });

                DynamicMethod dm = new DynamicMethod("emitCreate", typeof(object), null);
                ILGenerator il = dm.GetILGenerator();

                il.Emit(OpCodes.Newobj, ctor);
                il.Emit(OpCodes.Ret);

                _create = (Create)dm.CreateDelegate(typeof(Create));
            }
            return _create();
        }

        public static void DoTest1()
        {
            Console.WriteLine("ReflectionCreate Start.");
            object o = LambdaCreateObject();
        }

        // 使用Lambda表达式方法创建对象        --  .netframework version 3.5以上支持，4.0以上增强
        private static object LambdaCreateObject()
        {
            if (_create1 == null)
            {
                Type targetType = typeof(Department);
                ConstructorInfo ctor = targetType.GetConstructor(new Type[] { });

                // new targetType()
                var createExpression = Expression.New(ctor);

                // (object)(new targetType())
                var convertExpression = Expression.Convert(
                    createExpression, typeof(object));
                var lambda = Expression.Lambda<Func<object>>(convertExpression);

                _create1 = lambda.Compile();
            }

            return _create1();
        }
    }
}
