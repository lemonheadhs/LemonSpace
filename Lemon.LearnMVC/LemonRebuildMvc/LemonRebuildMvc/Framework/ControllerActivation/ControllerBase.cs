using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LemonRebuildMvc.Framework.ActionInvoke;

namespace LemonRebuildMvc.Framework.ControllerActivation
{
    public abstract class ControllerBase : IController
    {
        protected IActionInvoker ActionInvoker { get; set; }

        public ControllerBase()
        {
            ActionInvoker = new ControllerActionInvoker();
        }

        public void Execute(URLMapping.RequestContext requestContext)
        {
            ControllerContext context = new ControllerContext 
            {
                RequestContext = requestContext,
                Controller = this
            };
            string actionName = requestContext.RouteData.ActionName;
            this.ActionInvoker.InvokeAction(context, actionName);
        }
    }
}