using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewTest()
        {
            string str = "字串";
            return View((object)str);
        }

        public ActionResult PartialViewTest()
        {
            string str = "Partial字串";
            // PartialView的話，沒有layout
            return PartialView("ViewTest", (object)str);
        }

    }
}