using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Xml.Linq;
using System.Reflection;

namespace TestConsole.Test
{
    public class DynamicObjectStudy
    {
        public static void DoTest()
        {
            dynamic employee = new DynamicXNode("Employee");
            employee.Name = new DynamicXNode("Name");
            employee.Name.FirstName = "Henry";
            employee.Name.LastName = "Cui";
            employee.BirthDay = "1987-10-14";

            Console.WriteLine(((XElement)employee).ToString());// 为什么 (employee as XElement).ToString()会报错？

            Console.ReadKey();
        }



    }

    public class DynamicXNode : DynamicObject
    {
        private XElement _xElement;

        #region 构造方法
        public DynamicXNode(XElement node)
        {
            _xElement = node;
        }

        public DynamicXNode(string name)
        {
            _xElement = new XElement(name);
        }
        #endregion

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var node = _xElement.Element(binder.Name);
            if (node != null)
            {
                node.SetValue(value);
            }
            else
            {
                if (value is DynamicXNode)
                {
                    _xElement.Add(new XElement(binder.Name));
                }
                else
                {
                    _xElement.Add(new XElement(binder.Name, value.ToString()));
                }
            }
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var node = _xElement.Element(binder.Name);
            if (node != null)
            {
                result = new DynamicXNode(node);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var ttype = typeof(XElement);
            try
            {
                result = ttype.InvokeMember(binder.Name,
                    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                    null,
                    _xElement,
                    args);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type.Equals(typeof(XElement)))
            {
                result = _xElement;
                return true;
            }
            return base.TryConvert(binder, out result);
        }
    }
}
