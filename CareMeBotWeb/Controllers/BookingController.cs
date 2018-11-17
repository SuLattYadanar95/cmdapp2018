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
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> UpsertPatient(tbPatient patient, int doctorid = 0, string doctorname = null,
            DateTime? appdate = null, DateTime? fromtime = null, DateTime? totime = null, int scheduleid = 0, int hospitalID=0)
        {
            PatientAppointmentViewModel shvm = new PatientAppointmentViewModel();
            shvm.appointment = new tbAppointment();
            shvm.appointment.DoctorId = doctorid;
            shvm.appointment.DoctorName = doctorname;
            string date = appdate.Value.ToShortDateString();
            string fromtimedate = fromtime.Value.ToShortTimeString();
            string appointmentdate = date + " " + fromtimedate;
            shvm.appointment.ScheduleDataID = scheduleid;
            shvm.appointment.AppointmentDateTime = DateTime.Parse(appointmentdate);
            shvm.appointment.HospitalId = hospitalID; //CareMeClient.Helper.CookieHelper.getstaffHospitalID();
            //      shvm.appointment.HospitalName = CareMeClient.Helper.CookieHelper.getstaffHospitalName();

            shvm.patient = patient;
            PatientAppointmentViewModel result = await BookingApiRequestHelper.UpsertPatient(shvm);
            if (result != null)
            {
                BookingSuccessModel bsm = new BookingSuccessModel();
                bsm.doctor = await DoctorApiRequestHelper.GetDoctorById(result.appointment.DoctorId ?? 0);
                bsm.hospital = await HospitalApiRequestHelper.GetHospitalById(result.appointment.HospitalId ?? 0);
                bsm.scheduleData = await ScheduleApiRequestHelper.getScheduleDetail(result.appointment.ScheduleDataID ?? 0);
                bsm.pavm = result;
                return PartialView("_bookingSuccess", bsm);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult list(string msgrid=null)
        {
            ViewBag.msgrid = msgrid;
            return View("list");
        }


        public async Task<ActionResult> _bookedlist(string userid=null)
        {
            List<tbAppointment> appList = await BookingApiRequestHelper.getAppointments(userid);

            return PartialView("_bookedlist", appList);
        }


    }
}