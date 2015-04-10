using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LemonRebuildMvc.Framework.ControllerActivation;

namespace LemonRebuildMvc.Framework.URLMapping
{
    public class MvcRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MvcHandler(requestContext);
        }
    }
}