using CareMeClient.Helper;
using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CareMeClient.Controllers
{
    [Authorize]
    public class AppointmentBookingController : Controller
    {
        // GET: AppointmentBooking
        public async Task<ActionResult> Index()
        {
            // int hospitalid = 2;
            //List<tbDoctor> result = await DoctorApiRequestHelper.DoctorList(hospitalid);
            return View();
        }

        //public ActionResult _appointmentBookingList()
        //{
        //    return PartialView("_appointmentBookingList");
        //}

        public async Task<ActionResult> _appointmentBookingList(string name = null, string status = null, int pagesize = 10, int page = 1, DateTime? datetime = null, DateTime? time = null, int hospitalid = 0, int doctorid = 0)
        {
            hospitalid = CookieHelper.getstaffHospitalID();
            PagedListClient<tbAppointment> result = await AppointmentApiRequestHelper.GetAppointmentWithPaging(name, status, pagesize, page, datetime, time, hospitalid, doctorid);
            return PartialView("_appointmentBookingList", result);
        }

        public async Task<ActionResult> _newstatusChange(int id, string statuschange = null, int pagesize = 10, int page = 1)
        {
            PagedListClient<tbAppointment> result = await AppointmentApiRequestHelper.NewStatusChange(id, statuschange, pagesize, page);
            return PartialView("_appointmentBookingList", result);
        }

        public async Task<ActionResult> timelist(int doctorid = 0, DateTime? datetime = null)
        {
            datetime = datetime.Value.Date;
            List<tbScheduleData> result = await AppointmentApiRequestHelper.timelist(doctorid, datetime);
            return PartialView("timelist", result);  //Json(result, JsonRequestBehavior.AllowGet);
        }

        //public async Task<ActionResult> doctorlist(int hospitalid = 0, DateTime? datetime = null)
        //{
        //    hospitalid = CookieHelper.getstaffHospitalID();
        //    List<tbDoctor> result = await AppointmentApiRequestHelper.doctorlist(hospitalid, datetime);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public async Task<ActionResult> doctorlist(int hospitalid = 0, DateTime? datetime = null)
        {
            hospitalid = CookieHelper.getstaffHospitalID();
            List<tbDoctor> result = await AppointmentApiRequestHelper.doctorlist(hospitalid, datetime);
            //   return Json(result, JsonRequestBehavior.AllowGet);
            return PartialView("_doctorList", result);
        }
    }
}