using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LemonRebuildMvc.Framework.URLMapping;

namespace LemonRebuildMvc.Framework.ControllerActivation
{
    public class MvcHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }
        public RequestContext RequestContext { get; private set; }

        public MvcHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            string controllerName = RequestContext.RouteData.Controller;
            IControllerFactory controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = controllerFactory.CreateController(RequestContext, controllerName);
            controller.Execute(this.RequestContext);
        }
    }
}