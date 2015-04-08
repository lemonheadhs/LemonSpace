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
                .AppRelativeCurrentExecutionFilePath.Substring(2), out variables)) //TODO:为什么我在测试的时候，模拟的httpcontext里面，AppRelativeCurrentExecutionFilePath属性会抛异常？
            {
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