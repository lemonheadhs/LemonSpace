using LemonRebuildMvc.Framework.URLMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Threading;
using System.Diagnostics;

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
        ///UrlRoutingModule 构造函数 的测试
        ///</summary>
        //[TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("D:\\GitHub\\LemonSpace\\Lemon.LearnMVC\\LemonRebuildMvc\\LemonRebuildMvc", "/")]
        //[UrlToTest("http://localhost:14314/")]
        //public void UrlRoutingModuleConstructorTest()
        //{
        //    UrlRoutingModule target = new UrlRoutingModule();
        //    Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        //}

        /// <summary>
        ///Dispose 的测试
        ///</summary>
        //[TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("D:\\GitHub\\LemonSpace\\Lemon.LearnMVC\\LemonRebuildMvc\\LemonRebuildMvc", "/")]
        //[UrlToTest("http://localhost:14314/")]
        //public void DisposeTest()
        //{
        //    UrlRoutingModule target = new UrlRoutingModule(); // TODO: 初始化为适当的值
        //    target.Dispose();
        //    Assert.Inconclusive("无法验证不返回值的方法。");
        //}

        /// <summary>
        ///Init 的测试
        ///</summary>
        //[TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("D:\\GitHub\\LemonSpace\\Lemon.LearnMVC\\LemonRebuildMvc\\LemonRebuildMvc", "/")]
        //[UrlToTest("http://localhost:14314/")]
        //public void InitTest()
        //{
        //    UrlRoutingModule target = new UrlRoutingModule(); // TODO: 初始化为适当的值
        //    HttpApplication context = null; // TODO: 初始化为适当的值
        //    target.Init(context);
        //    Assert.Inconclusive("无法验证不返回值的方法。");
        //}

        /// <summary>
        ///OnPostResolveRequestCache 的测试
        ///</summary>
        // TODO: 确保 UrlToTest 特性指定一个指向 ASP.NET 页的 URL(例如，
        // http://.../Default.aspx)。这对于在 Web 服务器上执行单元测试是必需的，
        //无论要测试页、Web 服务还是 WCF 服务都是如此。
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("D:\\GitHub\\LemonSpace\\Lemon.LearnMVC\\LemonRebuildMvc\\LemonRebuildMvc", "/")]
        //[UrlToTest("http://localhost:14314/lemon/test/WebForm1.aspx")]
        //[DeploymentItem("LemonRebuildMvc.dll")]
        public void OnPostResolveRequestCacheTest()
        {
            PrepareHttpContext();

            SimulateAppStart();


            UrlRoutingModule_Accessor target = new UrlRoutingModule_Accessor(); // TODO: 初始化为适当的值
            object sender = null; // TODO: 初始化为适当的值
            EventArgs e = null; // TODO: 初始化为适当的值
            //target.OnPostResolveRequestCache(sender, e);


            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        private void SimulateAppStart()
        {
            RouteTable.Routes.Add("default", new Route { Url = "{controller}/{action}" });

            //Debug.Assert(HttpRuntime.AppDomainAppVirtualPathObject != null);

            Trace.WriteLine(HttpContext.Current.Request.CurrentExecutionFilePath);
            Trace.WriteLine(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath);//这个属性一直出错，原来是HttpRuntime.AppDomainAppVirtualPath为空的原因
            // 只是HttpRuntime.AppDomainAppVirtualPath，不大好模拟还没找到办法
        }

        private void PrepareHttpContext()
        {
            Thread.GetDomain().SetData(".appPath", @"D:\GitHub\LemonSpace\Lemon.LearnMVC\LemonRebuildMvc\TestProject1\bin\Debug\");
            Thread.GetDomain().SetData(".appVPath", "/");

            TextWriter tw = new StringWriter();
            SimpleWorkerRequest wr = new SimpleWorkerRequest("lemon/test", "", tw);
            //SimpleWorkerRequest wr = new SimpleWorkerRequest("/", @"D:\GitHub\LemonSpace\Lemon.LearnMVC\LemonRebuildMvc\TestProject1\bin\Debug\", "lemon/test", "", tw);
            HttpContext.Current = new HttpContext(wr);
        }
    }
}
