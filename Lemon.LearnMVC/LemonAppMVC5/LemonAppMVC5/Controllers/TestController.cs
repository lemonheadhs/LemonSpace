using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LemonAppMVC5.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubWin(int t)
        {

            List<KeyValuePair<string, string>> list1 = new List<KeyValuePair<string, string>>();
            if (t==1)
            {
                list1.Add(new KeyValuePair<string, string>("123", "yao"));
                list1.Add(new KeyValuePair<string, string>("234", "li"));
                list1.Add(new KeyValuePair<string, string>("456", "zhou"));
            }
            else
            {
                list1.Add(new KeyValuePair<string, string>("abc", "成"));
                list1.Add(new KeyValuePair<string, string>("eee", "永"));
                list1.Add(new KeyValuePair<string, string>("hjk", "何"));
            }

            return View("SubWin1", list1);
        }
    }
}