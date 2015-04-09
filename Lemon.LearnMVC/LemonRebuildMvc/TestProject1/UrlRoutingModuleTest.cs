using LemonRebuildMvc.Framework.URLMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Threading;
using System.Diagnostics;
using TestProject1.LemonTestUtils;

namespace TestProject1
{
    
    
    /// <summary>
    ///这是 UrlRoutingModuleTest 的测试类，旨在
    ///包含所有 UrlRoutingModuleTest 单元测试
    ///</summary>
    [TestClass()]
    public class UrlRoutingModuleTest
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

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///OnPostResolveRequestCache 的测试
        ///</summary>
        //无论要测试页、Web 服务还是 WCF 服务都是如此。
        [TestMethod()]
        public void OnPostResolveRequestCacheTest()
        {
            WebTestUtil.PrepareHttpContext("lemon/test", "");

            SimulateAppStart();

            UrlRoutingModule_Accessor target = new UrlRoutingModule_Accessor(); 
            object sender = null; 
            EventArgs e = null; 
            target.OnPostResolveRequestCache(sender, e);
            
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        private void SimulateAppStart()
        {
            RouteTable.Routes.Add("default", new Route { Url = "{controller}/{action}" });

        }

        

        
    }
}
