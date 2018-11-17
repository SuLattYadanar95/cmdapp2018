using Core.Extensions;
using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CareMeBotWeb.Controllers
{
    public class HospitalController : Controller
    {
        // GET: Hospital
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> _hospitalList(int doctorID, int hospitalID = 0, DateTime? AppointmentDate = null)
        {
            AppointmentDate = MyExtension.getLocalTime(DateTime.UtcNow);
            IEnumerable<HospitalSchedules> hospitalList = await HospitalApiRequestHelper.scheduledHospitalList(doctorID, hospitalID, AppointmentDate);
            return PartialView("_hospitalList", hospitalList);
        }




    }
}