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
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> _doctor(int doctorID)
        {
            DoctorSpecialityViewModel doctor = await DoctorApiRequestHelper.GetDoctorById(doctorID);
            return PartialView("_doctor", doctor);
        }


    }
}