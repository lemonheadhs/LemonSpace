using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Collections.Specialized;

namespace TestProject1.LemonTestUtils
{
    public class WebTestUtil
    {
        const int MAX_TIMEOUT = 24 * 60;

        public static void PrepareHttpContext(string requestUrl, string queryString)
        {
            TextWriter tw = new StringWriter();
            PrepareHttpContext(requestUrl, queryString, tw);
        }

        private static void PrepareHttpContext(string requestUrl, string queryString, TextWriter tw)
        {
            //设置应用程序域中的预设值，模拟asp.net框架行为
            Thread.GetDomain().SetData(".appDomain", "*");
            Thread.GetDomain().SetData(".appPath", @"D:\GitHub\LemonSpace\Lemon.LearnMVC\LemonRebuildMvc\TestProject1\bin\Debug");
            Thread.GetDomain().SetData(".appVPath", "/");

            LemonWorkerRequest wr = new LemonWorkerRequest(requestUrl, queryString, tw);//这个tw参数其实没有办法设置到Response中去，它会被Response在初始化时替换常它自己new的writer
            HttpContext.Current = new HttpContext(wr);
        }

        public static void PrepareSession()
        {
            Type type = typeof(SessionStateUtility);
            MethodInfo method = type.GetMethod("CreateLegitStoreData", BindingFlags.NonPublic | BindingFlags.Static);
            SessionStateStoreData item = method.Invoke(null, new object[] { HttpContext.Current, null, null, MAX_TIMEOUT }) as SessionStateStoreData;

            HttpSessionStateContainer sessionContainner =
                new HttpSessionStateContainer("_s_id", item.Items, item.StaticObjects, item.Timeout, false, HttpCookieMode.UseUri, SessionStateMode.InProc, false);
            SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, sessionContainner);

        }

        public static void SwitchFormToWritable(NameValueCollection form)
        {
            Type type = typeof(NameValueCollection);

            FieldInfo fieldInfo = type.BaseType.GetField("_readOnly", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(form, false);

            //PropertyInfo property = type.BaseType.GetProperty("IsReadOnly", BindingFlags.NonPublic|BindingFlags.SetProperty|BindingFlags.GetProperty);  为什么找不到？
            //property.SetValue(form, false);
        }

        public static void FillFormWithString(NameValueCollection form, string initStr)
        {
            Assembly assem = Assembly.Load("System.Web.dll");
            Type type = assem.GetType("HttpValueCollection");
            FieldInfo fieldInfo = type.BaseType.BaseType.GetField("_readOnly", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(form, false);
            PropertyInfo property = type.GetProperty("IsReadOnly", BindingFlags.NonPublic);
            property.SetValue(form, false);

            MethodInfo methodInfo = type.GetMethod("FillFromString", 
                                                BindingFlags.NonPublic | BindingFlags.Instance, 
                                                null, 
                                                new Type[] { typeof(string) }, 
                                                null);
            methodInfo.Invoke(form, new object[]{ initStr });
            
        }

        public static void ResponseSwitchWriter(TextWriter tw)
        {
            var response = HttpContext.Current.Response;
            Type type = response.GetType();
            FieldInfo filedInfo = type.GetField("_writer", BindingFlags.NonPublic | BindingFlags.Instance);
            filedInfo.SetValue(response, tw);
        }
    }
}
