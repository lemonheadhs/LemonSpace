using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LemonRebuildMvc.Framework.ControllerActivation;
using LemonRebuildMvc.Framework.URLMapping;

namespace LemonRebuildMvc.Framework.ActionInvoke
{
    public class ControllerContext
    {
        public ControllerBase Controller { get; set; }
        public RequestContext RequestContext { get; set; }
    }
}