using LemonRebuildMvc.Framework.ControllerActivation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using LemonRebuildMvc.Framework.URLMapping;
using System.Web;
using System.Web.Compilation;
using System.Reflection;
using TestProject1.LemonTestUtils;
using LemonRebuildMvc;
using Moq;

namespace TestProject1
{
    
    
    /// <summary>
    ///这是 MvcHandlerTest 的测试类，旨在
    ///包含所有 MvcHandlerTest 单元测试
    ///</summary>
    [TestClass()]
    public class MvcHandlerTest
    {
        public static string TestResultStr { get; set; }

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
        ///ProcessRequest 的测试
        ///</summary>
        [TestMethod]
        public void ProcessRequestTest()
        {
            WebTestUtil.PrepareHttpContext("lemon/test", "age=25");
            SimulateAppStart();
            
            RequestContext requestContext = new RequestContext 
            { 
                HttpContext = new HttpContextWrapper(HttpContext.Current), 
                RouteData = new RouteData() 
            };
            requestContext.RouteData.Values.Add("controller", "lemon");
            requestContext.RouteData.Values.Add("action", "test");
            MvcHandler target = new MvcHandler(requestContext);
            HttpContext context = HttpContext.Current;

            string expected = "/lemon/test?age=25";
            target.ProcessRequest(context);

            Assert.AreEqual(expected, MvcHandlerTest.TestResultStr);
        }

        private static void SimulateAppStart()
        {
            //Type type = typeof(BuildManager);
            //MethodInfo methodInfo = type.GetMethod("ExecutePreAppStart", BindingFlags.NonPublic | BindingFlags.Static);
            //methodInfo.Invoke(null, null);

            ControllerBuilder.Current.SetControllerFactory(new TestContrllerFactory());
            ControllerBuilder.Current.DefaultNamespaces.Add("LemonRebuildMvc");
        }


    }

    public class TestContrllerFactory : IControllerFactory
    {

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            var mo = new Mock<IController>();
            mo.Setup(foo => foo.Execute(It.IsNotNull<RequestContext>()))
              .Callback<RequestContext>(context => 
              {
                  MvcHandlerTest.TestResultStr = context.HttpContext.Request.RawUrl;
              });
            return mo.Object;
        }
    }
}
