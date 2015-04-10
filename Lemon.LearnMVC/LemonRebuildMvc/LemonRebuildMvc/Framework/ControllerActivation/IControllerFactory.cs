using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LemonRebuildMvc.Framework.URLMapping;

namespace LemonRebuildMvc.Framework.ControllerActivation
{
    public interface IControllerFactory
    {
        IController CreateController(RequestContext requestContext, string controllerName);
    }
}
