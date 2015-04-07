using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.URLMapping
{
    public class RouteCollection : Dictionary<string, RouteBase>
    {
        public RouteData GetRouteData(HttpContextBase context)
        {
            foreach (var route in this.Values)
            {
                RouteData routeData = route.GetRouteData(context);
                if (routeData != null)
                {
                    return routeData;
                }
            }
            return null;
        }
    }
}