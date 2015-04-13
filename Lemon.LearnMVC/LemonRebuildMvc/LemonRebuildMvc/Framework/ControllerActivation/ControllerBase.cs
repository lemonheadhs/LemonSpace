using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.ControllerActivation
{
    public class ControllerBase : IController
    {
        public void Execute(URLMapping.RequestContext requestContext)
        {
            throw new NotImplementedException();
        }
    }
}