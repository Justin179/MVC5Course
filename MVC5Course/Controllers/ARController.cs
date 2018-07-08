using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ActionResult ContentTest()
        {
            return Content("Test Content!",
                "text/plain",
                Encoding.GetEncoding("Big5"));
        }

        public ActionResult FileTest(string dl)
        {
            if (String.IsNullOrEmpty(dl))
            {
                return File(Server.MapPath("~/App_Data/city_cafe.png"),
                    "image/jpeg");
            }
            else
            {
                return File(Server.MapPath("~/App_Data/city_cafe.png"),
                    "image/jpeg", "xxx_cafe.png");
            }
        }

    }
}