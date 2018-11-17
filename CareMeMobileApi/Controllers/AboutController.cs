using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareMeMobileApi.Controllers
{
    public class ReadMeController : Controller
    {
        // GET: ReadMe
        public ActionResult Termsofservices()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult HelpAndSupport()
        {
            return View();
        }
    }
}