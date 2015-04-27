using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LemonRebuildMvc.Framework.ActionInvoke;
using LemonRebuildMvc.Framework.ControllerActivation;
using System.IO;

namespace LemonRebuildMvc
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index(SimpleModel model)
        {
            Action<TextWriter> callback = tw =>
            {
                tw.Write(string.Format("Controller:{0}, Action:{1}<br/>", model.Controller, model.Action));
                tw.Write(string.Format("Foo:{0}, <br/>Bar:{1}, <br/>Baz:{2}", model.Foo, model.Bar, model.Baz));
            };
            return new RawContentResult(callback);
        }
    }
}