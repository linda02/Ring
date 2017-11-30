using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Utility;

namespace Ring.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<int> list=new List<int>();
            int num = 10;
            for (int i = 0; i < num; i++)
            {
                list.Add(i);
            }
            string s = StringHelper.GetArrayStr(list, ",");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}