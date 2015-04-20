using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storage.Controllers
{
    [HandleError]
    public class ViewController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Order()
        {
            return PartialView();
        }

        public PartialViewResult Material()
        {
            return PartialView();
        }

        public PartialViewResult Detail()
        {
            return PartialView();
        }

        public PartialViewResult Product()
        {
            return PartialView();
        }

    }
}