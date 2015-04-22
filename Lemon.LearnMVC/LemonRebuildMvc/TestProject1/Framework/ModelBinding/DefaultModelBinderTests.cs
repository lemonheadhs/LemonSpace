using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonRebuildMvc.Framework.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LemonRebuildMvc.Framework.ActionInvoke;
using TestProject1.LemonTestUtils;
using LemonRebuildMvc.Framework.URLMapping;
using System.Web;

namespace TestProject1
{
    [TestClass()]
    public class DefaultModelBinderTests
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


        [TestMethod()]
        public void BindModelTest()
        {
            DefaultModelBinder target = new DefaultModelBinder();

            WebTestUtil.PrepareHttpContext("lemon/test", "name=lobster&count=1");
            RouteData routeData = new RouteData();
            routeData.Values.Add("orderTime", "2015-04-22");
            routeData.DataTokens.Add("restraurant", "大排挡");
            HttpContext.Current.Request.Form.Add("weight", "20");//TODO:还要再mock一下 Form对象
            ControllerContext controllerContext = new ControllerContext();
            controllerContext.RequestContext = new RequestContext();
            controllerContext.RequestContext.HttpContext = new HttpContextWrapper(HttpContext.Current);
            controllerContext.RequestContext.RouteData = routeData;
            string modelName = "AFeast";
            Type modelType = typeof(SeaFood);

            SeaFood expected = new SeaFood();
            expected.Name = "lobster";
            expected.Weight = 20;
            expected.OrderTime = DateTime.Parse("2015-04-22");
            SeaFood accept = null;

            object r = target.BindModel(controllerContext, modelName, modelType);
            accept = (SeaFood)r;

            Assert.AreEqual(expected, accept);            
            
        }

        public class SeaFood
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public DateTime OrderTime { get; set; }

            public override bool Equals(object obj)
            {
                SeaFood othr = obj as SeaFood;
                if (othr != null)
                {
                    if(this.Name==othr.Name 
                        && this.Weight==othr.Weight
                        && this.OrderTime.Date.Equals(othr.OrderTime.Date))
                    {
                        return true;
                    }
                }
                return false ;
            }
        }
    }
}
