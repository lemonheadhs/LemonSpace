using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LemonRebuildMvc.Framework.ActionInvoke;
using System.Text;
using System.IO;
using Moq;
using System.Web;
using LemonRebuildMvc.Framework.URLMapping;
using LemonRebuildMvc;
using TestProject1.LemonTestUtils;

namespace TestProject1
{
    [TestClass]
    public class ControllerActionInvokerTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        /// InvokeAction 的测试
        /// </summary>
        [TestMethod]
        public void InvokeActionTest()
        {
            ControllerActionInvoker target = new ControllerActionInvoker();

            string expected = "Controller:home, Action:index<br/>Foo:1, <br/>Bar:2, <br/>Baz:3.4";

            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);

            WebTestUtil.PrepareHttpContext("home/index", "foo=1&bar=2&baz=3.4");
            WebTestUtil.ResponseSwitchWriter(writer);

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "home");
            routeData.Values.Add("action", "index");
            var context = new ControllerContext
            {
                RequestContext = new RequestContext 
                    { 
                        HttpContext = new HttpContextWrapper(HttpContext.Current),
                        RouteData = routeData
                    },
                Controller = new HomeController()
            };
            string actionName = "index";
            target.InvokeAction(context, actionName);

            writer.Flush();

            Assert.AreEqual(expected, sb.ToString());
            
        }
    }
}
