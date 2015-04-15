using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LemonRebuildMvc.Framework.ActionInvoke;

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
            ControllerContext context = null;//TODO:
            string actionName = "";//TODO:
            target.InvokeAction(context, actionName);

            Assert.Inconclusive();
        }
    }
}
