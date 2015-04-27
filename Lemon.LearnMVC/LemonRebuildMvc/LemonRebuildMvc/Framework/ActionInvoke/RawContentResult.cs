using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LemonRebuildMvc.Framework.ActionInvoke
{
    public class RawContentResult : ActionResult
    {
        private Action<TextWriter> callBack;

        public RawContentResult(Action<TextWriter> CallBack)
        {
            this.callBack = CallBack;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            this.callBack(context.RequestContext.HttpContext.Response.Output);
        }
    }
}