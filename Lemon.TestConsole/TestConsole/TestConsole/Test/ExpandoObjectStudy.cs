using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Xml.Linq;

namespace TestConsole.Test
{
    //
    // 使用了此链接下博客的代码进行学习，多谢原作者Henry Cui！
    // http://www.cnblogs.com/Henllyee/tag/dynamic/
    //

    public class ExpandoObjectStudy
    {
        //除了在运行时添加公共属性、公共方法，居然还可以添加事件！
        public static void DoTest()
        {
            dynamic employee = new ExpandoObject();// 这里要使用dynamic， ExpandoObject employee = new ExpandoObject();会令下面的代码出现编译时错误
            employee.FirstName = "Henry";
            employee.LastName = "Cui";
            employee.Age = 23;
            employee.SayHello = (Action)(() => //因为是动态类型，编译器分析不出这个lambda表达式的委托签名，所以需要强制类型转换
            {
                Console.WriteLine("{0} say \"Hello\" at {1}",
                    employee.FirstName + " " + employee.LastName,
                    DateTime.Now.ToString());
            });
            employee.GetSalary = (Func<int, decimal>)((month) => 
            {
                if (month > 8)
                {
                    return 5000;
                }
                return 4000;
            });
            employee.AskForLeaveEvent = null; // 如果不加这一句，下面的代码会出现RunTimeError：“System.Dynamic.ExpandoObject”未包含“AskForLeaveEvent”的定义
            employee.AskForLeaveEvent += new EventHandler(OnEmployeeLeave);
            employee.AskForLeaveEvent(employee, new EventArgs());

            ShowAllMembers(employee);

            Console.WriteLine("---------------------------------------");
            ((IDictionary<string, object>)employee).Remove("AskForLeaveEvent");
            ShowAllMembers(employee);
            
            Console.ReadKey();
        }
        
        //使用 ExpandoObject来进行简单的Xml操作
        public static void DoTest1()
        {
            dynamic employee = new ExpandoObject();
            employee.FistName = "Henry";
            employee.LastName = "Cui";
            employee.Age = 23;
            employee.Company = new ExpandoObject();
            employee.Company.Name = "XXXX";
            employee.Company.Address = "Suzhou China";

            var xNode = ConvertExpandoObjectToXelement("Employee", employee);
            Console.WriteLine(xNode.ToString());

            Console.ReadKey();
        }

        public static void OnEmployeeLeave(object sender, EventArgs e)
        {
            dynamic em = (dynamic)sender;
            Console.WriteLine("Report Manager:{0} is asking for leave", em.FirstName + " " + em.LastName);
        }

        private static void ShowAllMembers(dynamic em)
        {
            foreach (var pro in (IDictionary<string, object>)em)
            {
                Console.WriteLine(pro.Key + " " + pro.Value);
            }
        }

        private static XElement ConvertExpandoObjectToXelement(string eleName, dynamic node)
        {
            var xNode = new XElement(eleName);
            foreach (var pro in (IDictionary<string, object>)node)
            {
                if (pro.Value is ExpandoObject)
                {
                    xNode.Add(ConvertExpandoObjectToXelement(pro.Key, pro.Value));
                }
                else
                {
                    xNode.Add(new XElement(pro.Key, pro.Value));
                }
            }
            return xNode;
        }
    }
}
