using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.URLMapping
{
    public class RequestContext
    {
        public RouteData RouteData { get; set; }
        public HttpContextBase HttpContext { get; set; } 
    }
}