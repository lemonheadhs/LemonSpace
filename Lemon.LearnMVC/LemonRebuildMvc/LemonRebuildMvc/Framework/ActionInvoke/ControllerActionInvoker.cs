using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace LemonRebuildMvc.Framework.ActionInvoke
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public void InvokeAction(ControllerContext controllerContext, string actionName)
        {
            throw new NotImplementedException();
            MethodInfo methodInfo = controllerContext.Controller.GetType().GetMethods().First(s => string.Compare(s.Name, actionName, true) == 0);



        }
    }
}