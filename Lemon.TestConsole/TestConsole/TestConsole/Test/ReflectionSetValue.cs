using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace TestConsole.Test
{
    public class ReflectionSetValue
    {
        private static Action<object, string> _set = null;

        public static void DoTest()
        {
            Console.WriteLine("ReflectionSetValue Start.");

            Department dep = new Department();
            //ReflectionSet(dep, "DepName", "lemon");

            LambdaSet(dep, "DepName", "lemon");
        }

        // 使用传统反射方式设置属性值
        private static void ReflectionSet(object target, string propertyName, string pValue)
        {
            throw new NotImplementedException();
        }

        // 使用lambda方式设置属性值
        private static void LambdaSet(object target, string propertyName, string pValue)
        {
            if (_set == null)
            {
                #region 可以这样写
                Type targetType = target.GetType();
                PropertyInfo property = targetType.GetProperty(propertyName);

                // 表达式【 ((targetType)target).propery = (propertyType)pValue】
                var exp_1_1_1 = Expression.Parameter(typeof(object), "instance");
                var exp_1_1 = Expression.Convert(exp_1_1_1, targetType);
                MemberExpression exp_1 = Expression.Property(exp_1_1, property);

                var exp_2_1 = Expression.Parameter(typeof(string), "input");
                var exp_2 = Expression.TypeAs(exp_2_1, property.PropertyType); // 这里要使用 PropertyInfo.PropertyType
                // 试了几个， property.ReflectType， property.GetType()  都不对

                var exp = Expression.Assign(exp_1, exp_2);
                var lambda = Expression.Lambda<Action<object, string>>(exp, new ParameterExpression[] { exp_1_1_1, exp_2_1 });
                _set = lambda.Compile();
                #endregion


                #region 还可以这样写 吗？？
                //var v =(from propertyType in target.GetType().GetProperty(propertyName) 
                //         let targetExp = Expression.Parameter(typeof(object))
                //         let castTargetExp = Expression.Convert(targetExp, target.GetType())
                //         let propertyExp = Expression.Property(castTargetExp, propertyType)
                //         let inputExp = Expression.Parameter(typeof(string))
                //         let castInputExp = Expression.Convert(inputExp, propertyType)
                //         select Expression.Lambda<Action<object, string>>(
                //                Expression.Assign(propertyExp, castInputExp),
                //                targetExp,
                //                inputExp
                //            ).Compile());
                #endregion

            }
            _set(target, pValue);
        }

    }
}
