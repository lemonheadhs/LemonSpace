using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.URLMapping
{
    public class Route : RouteBase
    {
        public string Url { get; set; }

        public override RouteData GetRouteData(HttpContextBase context)
        {
            throw new NotImplementedException();
            IDictionary<string, object> variables;
            if (this.Match(context.Request
                .AppRelativeCurrentExecutionFilePath.Substring(2), out variables))
            {
                RouteData routeData = new RouteData();
                foreach (var item in variables)
                {
                    routeData.Values.Add(item.Key, item.Value);
                }
            }
        }

        private bool Match(string requestUrl, out IDictionary<string, object> variables)
        {
            throw new NotImplementedException();
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