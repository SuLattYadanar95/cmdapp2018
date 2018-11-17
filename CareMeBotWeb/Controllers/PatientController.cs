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
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> _PatientForm(string FormType, int ID, int doctorid = 0,
            string doctorname = null, DateTime? appdate = null,
            DateTime? fromtime = null, DateTime? totime = null, 
            int scheduleid = 0, string MsgrID = null, string MsgrName=null)
        {
            ViewBag.doctorid = doctorid;
            ViewBag.doctorname = doctorname;
            ViewBag.appdate = appdate;
            ViewBag.fromtime = fromtime;
            ViewBag.totime = totime;
            ViewBag.scheduleid = scheduleid;
            tbPatient patient = new tbPatient();
            patient.MsgrID = MsgrID;
            patient.MsgrName = MsgrName;
            if (FormType == "Add")
            {
                return PartialView("_PatientForm", patient);
            }
            else
            {
                return null;
            }
        }


        public async Task<ActionResult> UpsertPatient(tbPatient patient, int doctorid = 0, 
            string doctorname = null, DateTime? appdate = null, DateTime? fromtime = null,
            DateTime? totime = null, int scheduleid = 0, int hospitalID=0)
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
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }
    }
}