using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Reflection;

namespace TestProject1.LemonTestUtils
{
    public class WebTestUtil
    {
        public static void PrepareHttpContext(string requestUrl, string queryString)
        {
            //设置应用程序域中的预设值，模拟asp.net框架行为
            Thread.GetDomain().SetData(".appDomain", "*");
            Thread.GetDomain().SetData(".appPath", @"D:\GitHub\LemonSpace\Lemon.LearnMVC\LemonRebuildMvc\TestProject1\bin\Debug");
            Thread.GetDomain().SetData(".appVPath", "/");

            TextWriter tw = new StringWriter();
            LemonWorkerRequest wr = new LemonWorkerRequest(requestUrl, queryString, tw);
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
    }
}
