using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TestConsole.FromJeffreyZhao.CodeTimer;
using System.Reflection.Emit;

namespace TestConsole.Test
{
    public class ReflectionGetValue
    {
        private static Func<object, string, object> emitGet;

        // 比较反射方法获得属性的性能
        public static void DoTest()
        {
            Console.WriteLine("ReflectionGetValue Start.");
            Department dep = new Department { DepName = "whatever" };

            CodeTimer.Initialize();
            CodeTimer.Time("ReflectionGet", 1, 
                () => 
                {
                    for (int n = 0; n < 1000000; n++)
                    {
                        object s = ReflectionGet(dep, "DepName"); 
                    }
                });
            CodeTimer.Time("DirectGet", 1,
                () =>
                {
                    for (int n = 0; n < 1000000; n++)
                    {
                        string s = dep.DepName;
                    }
                });
            CodeTimer.Time("EmitGet", 1,
                () =>
                {
                    for (int n = 0; n < 1000000; n++)
                    {
                        object s = EmitGet(dep, "DepName");
                    }
                });
            Console.ReadKey();
        }

        public static void DoTest1()
        {
            Console.WriteLine("ReflectionGetValue Start.");
            Department dep = new Department { DepName = "whatever" };

            object s = EmitGet(dep, "DepName");
        }

        // 传统的反射方法获取属性
        private static object ReflectionGet(object target, string propertyName)
        {
            Type type = target.GetType();
            PropertyInfo _property = type.GetProperty(propertyName);

            if (_property != null)
            {
                return _property.GetValue(target, null);
            }
            
            return null;
        }

        // emit方法优化后的获取属性
        private static object EmitGet(object target, string propertyName)
        {
            if (emitGet == null)
            {
                Type targetType = target.GetType();
                // 创建动态方法
                DynamicMethod dm = new DynamicMethod("emitGet", typeof(object), new Type[] { typeof(object), typeof(string) });
                ILGenerator il = dm.GetILGenerator();

                PropertyInfo propertyInfo = targetType.GetProperty(propertyName);
                MethodInfo targetPropertyGet = propertyInfo.GetGetMethod();

                //il.DeclareLocal(typeof(object));
                il.Emit(OpCodes.Ldarg_0);                               // 加载第一个参数到寄存器 -- 也就是加载target
                il.Emit(OpCodes.Castclass, targetType);                 // 对target进行类型转换，从object还原到本来的类型
                il.EmitCall(OpCodes.Callvirt, targetPropertyGet, null); // 对target调用取属性值的方法
                if (targetPropertyGet.ReturnType.IsValueType)// 如果属性是值类型，就需要进行装箱，因为返回类型是object
                {
                    il.Emit(OpCodes.Box, targetPropertyGet.ReturnType); // 装箱
                }
                //il.Emit(OpCodes.Stloc_0); //Store it
                //il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ret);                                   // 将寄存器上的值返回

                // 动态方法创建一个委托保存起来
                emitGet = (Func<object, string, object>)dm.CreateDelegate(typeof(Func<object, string, object>));
            }
            
            return emitGet(target, propertyName);
        }
    }

    public class Department
    {
        public int DepNO { get; set; }
        public string DepName { get; set; }
        public string Loc { get; set; }
    }
}
