using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.URLMapping
{
    public class Route : RouteBase
    {
        public IRouteHandler RouteHandler { get; set; }
        public string Url { get; set; }
        public IDictionary<string, object> DataToken { get; set; }

        public Route()
        {
            this.DataToken = new Dictionary<string, object>();
            this.RouteHandler = new MvcRouteHandler();
        }

        public override RouteData GetRouteData(HttpContextBase context)
        {
            IDictionary<string, object> variables;
            RouteData routeData = null;
            if (this.Match(context.Request
                .AppRelativeCurrentExecutionFilePath.Substring(2), out variables)) 
            {
                //为什么我在测试的时候，模拟的httpcontext里面，AppRelativeCurrentExecutionFilePath属性会抛异常？
                //  因为，我使用了SimpleWorkerRequest类来模拟初始化HttpContext，虽然我设置了 ".appPath",".appVPath"两个应用程序域预存值，
                //使得swr类可以工作，但是HttpRequest类中也依赖这两个预存值，却没有正确初始化。
                //这里的原因就在于，HttpRequest等类要获取appPath之类的信息时，会向HttpRuntime查询这类信息；
                //HttpRuntime会在程序集加载的时候，查询应用程序域预存值，来初始化appPath之类的信息，但前提是，
                // asp.net框架已经向应用程序域设置了这些预存值；而我没有模拟asp.net框架设置这些预存值，HttpRuntime没有正确初始化，导致在HttpRequest查询相应信息时报错
                // 这样，在我增加了".appDomain"预存值后，HttpRuntime得以正确初始化，问题解决

                routeData = new RouteData();
                foreach (var item in variables)
                {
                    routeData.Values.Add(item.Key, item.Value);
                }
                foreach (var item in DataToken)
                {
                    routeData.DataTokens.Add(item.Key, item.Value);
                }
                routeData.RouteHandler = RouteHandler;
            }
            return routeData;
        }

        private bool Match(string requestUrl, out IDictionary<string, object> variables)
        {
            variables = new Dictionary<string, object>();
            string[] strArray1 = requestUrl.Split('/');
            string[] strArray2 = this.Url.Split('/');

            if (strArray1.Length != strArray2.Length)
            {
                return false;
            }
            for (int n = 0; n < strArray2.Length; n++)
            {
                if (strArray2[n].StartsWith("{") && strArray2[n].EndsWith("}"))
                {
                    variables.Add(strArray2[n].Trim("{}".ToCharArray()), 
                                  strArray1[n]);
                }
            }
            return true;
        }
    }
}