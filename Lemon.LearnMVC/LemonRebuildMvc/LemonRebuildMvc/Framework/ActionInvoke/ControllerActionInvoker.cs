using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using LemonRebuildMvc.Framework.ModelBinding;

namespace LemonRebuildMvc.Framework.ActionInvoke
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public IModelBinder ModelBinder { get; private set; }

        public ControllerActionInvoker()
        {
            ModelBinder = new DefaultModelBinder();
        }

        public void InvokeAction(ControllerContext controllerContext, string actionName)
        {
            MethodInfo methodInfo = controllerContext.Controller.GetType().GetMethods().First(s => string.Compare(s.Name, actionName, true) == 0);
            List<object> parameters = new List<object>();
            foreach (var paraInfo in methodInfo.GetParameters())
            {
                parameters.Add(ModelBinder.BindModel(controllerContext, paraInfo.Name, paraInfo.ParameterType));
            }
            ActionResult actionResult = (ActionResult)methodInfo.Invoke(controllerContext.Controller, parameters.ToArray());
            actionResult.ExecuteResult(controllerContext);

        }
    }
}