using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareMeBotWeb.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index(int doctorID= 1, string msgrid = null, string msgrname= null)
        {
            ViewBag.msgrid = msgrid;
            ViewBag.msgrname = msgrname;
            ViewBag.doctorID = doctorID;
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}