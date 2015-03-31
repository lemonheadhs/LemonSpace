using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test.SqlWhere.Common
{
    public class ObjectProbe
    {
        public static bool HasReadableProperty(object target, string propertyName)
        {
            // 首先要取类型，然后看看缓存中是否存在该类型，没有就反射后加入缓存
            // 然后 判断类型有没有同名的可读属性
            throw new NotImplementedException();
        }
    }
}
