using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Compilation;

namespace LemonRebuildMvc.Framework.ControllerActivation
{
    public class DefaultControllerFactory : IControllerFactory
    {
        private static List<Type> controllerTypes = new List<Type>();
        static DefaultControllerFactory()
        {
            foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
            {
                foreach (Type type in assembly.GetTypes().Where(t => typeof(IController).IsAssignableFrom(type)))
                {
                    controllerTypes.Add(type);
                }
            }
        }

        public IController CreateController(URLMapping.RequestContext requestContext, string controllerName)
        {
            throw new NotImplementedException();
        }
    }
}