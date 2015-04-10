using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LemonRebuildMvc.Framework.URLMapping;

namespace LemonRebuildMvc.Framework.ControllerActivation
{
    public interface IController
    {
        void Execute(RequestContext requestContext);
    }
}
