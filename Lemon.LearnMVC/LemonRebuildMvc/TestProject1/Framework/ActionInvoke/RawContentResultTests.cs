using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonRebuildMvc.Framework.ActionInvoke;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Moq;
using System.Web;
using LemonRebuildMvc.Framework.URLMapping;

namespace TestProject1
{
    [TestClass()]
    public class RawContentResultTests
    {
        [TestMethod()]
        public void ExecuteResultTest()
        {
            string expected = "lemon test";
            Action<TextWriter> callback = (tw) => 
            {
                tw.Write(expected);
            };
            RawContentResult target = new RawContentResult(callback);

            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);

            var mo_HttpResponseBase = new Mock<HttpResponseBase>();
            mo_HttpResponseBase.Setup(foo => foo.Output)
                               .Returns(writer);
            var mo_HttpContextBase = new Mock<HttpContextBase>();
            mo_HttpContextBase.Setup(foo => foo.Response)
                              .Returns(mo_HttpResponseBase.Object);

            var context = new ControllerContext 
            {
                RequestContext = new RequestContext { HttpContext = mo_HttpContextBase.Object }
            };

            target.ExecuteResult(context);

            writer.Flush();

            Assert.AreEqual(expected, sb.ToString());
        }
    }
}
