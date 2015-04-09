using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Web;

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
    }
}
