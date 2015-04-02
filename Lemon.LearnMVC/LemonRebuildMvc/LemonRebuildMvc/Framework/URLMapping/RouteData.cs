using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.URLMapping
{
    public class RouteData
    {
        public IDictionary<string, string> Values { get; private set; }
        public IDictionary<string, string> DataTokens { get; private set; }
        public IRouteHandler RouteHandler { get; set; }

    }
}