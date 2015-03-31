using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace TestConsole.Test
{
    public static class EnumStudy     // 为了定义扩展方法，这个类必须是 static
    {
        public static void DoTest()
        {
            Console.WriteLine("EnumStudy Start:");

            // 枚举的遍历
            XXState[] arr = (XXState[])System.Enum.GetValues(typeof(XXState));
            foreach (XXState state in arr)
            {
                string s = state.ToString();
                Console.WriteLine(state.GetNameInChinese());
            }
        }

        /// <summary>
        /// 将一个枚举值转成object后，它的GetHashCode方法是哪个版本？
        /// </summary>
        public static void DoTest1()
        {
            string ss = (XXState.Draft as object).GetHashCode().ToString();
            bool s = false;
        }

        public enum XXState
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,

            /// <summary>
            /// 现行
            /// </summary>
            [Description("现行")]
            Issuance = 1,

            /// <summary>
            /// 废止
            /// </summary>
            [Description("废止")]
            Abolish = 2,
        }

        /// <summary>
        /// 使用扩展方法，为枚举提供得到中文名的方法
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetNameInChinese(this XXState state)
        {
            Type enumType = state.GetType();
            DescriptionAttribute attr;
            
            //attr = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute));  这个是获取 XXState上面的特性，而不是Draft上面的

            string name = System.Enum.GetName(enumType, state);
            FieldInfo info = enumType.GetField(name);

            attr = (DescriptionAttribute)Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute));

            if (attr != null && !string.IsNullOrEmpty(attr.Description))
            {
                return attr.Description;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
