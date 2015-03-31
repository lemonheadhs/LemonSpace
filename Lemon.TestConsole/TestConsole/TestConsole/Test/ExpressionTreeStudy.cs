using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace TestConsole.Test
{
    public class ExpressionTreeStudy
    {
        // 画出表达式树
        public static void DoTest()
        {
            //-a
            UnaryExpression exp1 = Expression.Negate(
                                    Expression.Parameter(typeof(int), "a")
                                    );

            //a + b * 2
            ParameterExpression exp2_1 = Expression.Parameter(typeof(int), "a");
            ParameterExpression exp2_2_1 = Expression.Parameter(typeof(int), "b");
            ConstantExpression exp2_2_2 = Expression.Constant(1, typeof(int));
            BinaryExpression exp2_2 = Expression.Multiply(exp2_2_1, exp2_2_2);
            BinaryExpression exp2 = Expression.Add(exp2_1, exp2_2);

            //Math.Sin(x) + Math.Cos(y)
            MethodCallExpression exp3_1 = Expression.Call(
                                                (typeof(Math)).GetMethod("Sin", BindingFlags.Public | BindingFlags.Static),
                                                Expression.Parameter(typeof(double), "x")
                                            );
            MethodCallExpression exp3_2 = Expression.Call(
                                                (typeof(Math)).GetMethod("Cos", BindingFlags.Public | BindingFlags.Static),
                                                Expression.Parameter(typeof(double), "y")
                                            );
            BinaryExpression exp3 = Expression.Add(exp3_1, exp3_2);

            //new StringBuilder(“Hello”)
            NewExpression exp4 = Expression.New(
                                            (typeof(StringBuilder)).GetConstructor(new Type[] { typeof(string) }),
                                            new Expression[] { Expression.Constant("Hello", typeof(string)) }
                                         );

            //new int[] { a, b, a + b}
            ParameterExpression exp5_1 = Expression.Parameter(typeof(int), "a");
            ParameterExpression exp5_2 = Expression.Parameter(typeof(int), "b");
            BinaryExpression exp5_3 = Expression.Add(exp5_1, exp5_2);
            NewArrayExpression exp5 = Expression.NewArrayInit(
                                                typeof(int),
                                                new Expression[] { exp5_1, exp5_2, exp5_3 }
                                            );

            //a[i – 1] * i
            ParameterExpression exp6_2 = Expression.Parameter(typeof(int), "i");
            BinaryExpression exp6_1_2 = Expression.Subtract(
                                                exp6_2,
                                                Expression.Constant(1, typeof(int))
                                            );
            ParameterExpression exp6_1_1 = Expression.Parameter(typeof(int[]), "a");
            IndexExpression exp6_1 = Expression.ArrayAccess(
                                                exp6_1_1,
                                                new Expression[] { exp6_1_2 }
                                            );
            BinaryExpression exp6 = Expression.Multiply(exp6_1, exp6_2);

            //a.Length > b | b >= 0
            ParameterExpression exp7_1_1_1 = Expression.Parameter(typeof(string), "a");
            MemberExpression exp7_1_1 = Expression.Property(
                                                exp7_1_1_1,
                                                typeof(string).GetProperty("Length", BindingFlags.Public | BindingFlags.Instance)
                                            );
            ParameterExpression exp7_1_2 = Expression.Parameter(typeof(int), "b");
            BinaryExpression exp7_1 = Expression.GreaterThan(exp7_1_1, exp7_1_2);
            BinaryExpression exp7_2 = Expression.GreaterThanOrEqual(
                                                exp7_1_2,
                                                Expression.Constant(0, typeof(int))
                                            );
            BinaryExpression exp7 = Expression.OrElse(exp7_1, exp7_2);

            //（高难度）new System.Windows.Point() { X = Math.Sin(a), Y = Math.Cos(a) }
            ParameterExpression exp8_a = Expression.Parameter(typeof(double), "a");
            MemberInitExpression exp8 = Expression.MemberInit(
                                                Expression.New(typeof(System.Windows.Point)),
                                                new MemberBinding[]{
                                                    Expression.Bind(
                                                        typeof(System.Windows.Point).GetProperty("X"), Expression.Call(typeof(Math).GetMethod("Sin"), exp8_a)),
                                                    Expression.Bind(
                                                        typeof(System.Windows.Point).GetProperty("Y"), Expression.Call(typeof(Math).GetMethod("Cos"), exp8_a))
                                                }
                                            );
        }

        public static void DoTest1()
        {
            //-a
            Expression<Func<int, int>> lambda1 = a => -a;

            //a + b * 2
            Expression<Func<int, int, int>> lambda2 = (a, b) => a + b * 2;

            //Math.Sin(x) + Math.Cos(y)
            Expression<Func<double, double, double>> lambda3 =
                                    (x, y) => Math.Sin(x) + Math.Cos(y);

            //new StringBuilder(“Hello”)
            Expression<Func<StringBuilder>> lambda4 = () => new StringBuilder("Hello");

            //new int[] { a, b, a + b}
            Expression<Func<int, int, int[]>> lambda5 = (a, b) => new int[] { a, b, a + b };

            //a[i – 1] * i
            Expression<Func<int[], int, int>> lambda6 = (a, i) => a[i - 1] * i;

            //a.Length > b | b >= 0

            //（高难度）new System.Windows.Point() { X = Math.Sin(a), Y = Math.Cos(a) }
        }

        public static void DoTest2()
        {
            //-a
            ParameterExpression exp8_a = Expression.Parameter(typeof(double), "a");
            MemberInitExpression exp8 = Expression.MemberInit(
                                                Expression.New(typeof(System.Windows.Point)),
                                                new MemberBinding[]{
                                                    Expression.Bind(
                                                        typeof(System.Windows.Point).GetProperty("X"), Expression.Call(typeof(Math).GetMethod("Sin"), exp8_a)),
                                                    Expression.Bind(
                                                        typeof(System.Windows.Point).GetProperty("Y"), Expression.Call(typeof(Math).GetMethod("Cos"), exp8_a))
                                                }
                                            );

            //Expression<Func<int, int, int[]>> lambda5 = (a, b) => new int[] { a, b, a + b };

            Console.WriteLine("1:{0}", exp8.ToString());
            //Console.WriteLine("2:{0}", lambda5.ToString());
            Console.ReadKey();
        }
    }
}
