using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.URLMapping
{
    public abstract class RouteBase
    {
        public abstract RouteData GetRouteData(HttpContextBase context);
    }
}