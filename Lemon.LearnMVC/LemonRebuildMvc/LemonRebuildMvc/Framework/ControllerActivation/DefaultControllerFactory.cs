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
                foreach (Type type in assembly.GetTypes().Where(t => typeof(IController).IsAssignableFrom(t)))
                {
                    controllerTypes.Add(type);
                }
            }
        }

        public IController CreateController(URLMapping.RequestContext requestContext, string controllerName)
        {
            string typeName = controllerName + "Controller";
            Type controllerType = controllerTypes.FirstOrDefault(s => string.Compare(s.Name, typeName, true) == 0);//名字相同，忽略大小写
            if (null == controllerType)
            {
                return null;
            }
            return (IController)Activator.CreateInstance(controllerType);
        }
    }
}