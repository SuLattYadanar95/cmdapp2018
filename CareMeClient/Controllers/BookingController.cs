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

namespace CareMeClient.Controllers
{
    public class BookingController : Controller
    {
      
        //Get Doctor Speciality List for Drop Down
        public async Task<ActionResult> Index()
        {
            DoctorSpecialitiesViewModel result = await DoctorApiRequestHelper.GetDocSpecialtyListFilter();
            return View(result);
        }

        //Get Doctor List By Schedule FromTime
        public async Task<ActionResult> DoctorListByTime(DateTime? fromtime = null, DateTime? totime = null, int pagesize = 0, int page = 0, int appointmentid=0, DateTime? date = null)
        {
            int hospitalid = Helper.CookieHelper.getstaffHospitalID();
            PagedListClient<tbDoctor> result = await BookingApiRequestHelper.DoctorListByTime(fromtime, totime,pagesize, page,hospitalid, appointmentid, date);
            return PartialView("_doctorListByScheduleTime", result);

        }

        //Get Appointment Schcedule Time by Schedule Date(Previous and Next)
        public async Task<ActionResult> AppointmentSchedule(DateTime? datetime = null, string type = null)
        {
            int hospitalid = Helper.CookieHelper.getstaffHospitalID();
            //   List<tbScheduleData> result = await DoctorApiRequestHelper.AppointmentSchedule(datetime, type);
            AppointScheduleViewModel result = await BookingApiRequestHelper.AppointmentSchedule(datetime, type, hospitalid);
            return PartialView("_scheduleTimeByDate", result);
        }

        //Get Doctor List By Schedule Date From Calender
        //public async Task<ActionResult> _doctor3listbyScheduleDate(DateTime? datetime = null, int pagesize = 5, int page = 1)
        //{
        //    PagedListClient<ScheduleDoctorViewModel> result = await Doctor3ApiRequestHelper.GetDoctorWithPaging(datetime, pagesize, page);
        //    return PartialView("_doctor3listbyScheduleDate", result);
        //}

        public async Task<ActionResult> _list(int docid = 0, int specialityid = 0, string docname = null, DateTime? datetime = null, DateTime? fromtime = null, DateTime? totime = null, int pagesize = 5, int page = 1)
        {
            int hospitalid = Helper.CookieHelper.getstaffHospitalID();
            PagedListClient<ScheduleBookingViewModel> result = await BookingApiRequestHelper.list(docid, specialityid, docname, datetime, fromtime, totime, pagesize, page,hospitalid);
            return PartialView("_list", result);
        }


        public async Task<ActionResult> getSpecialityList()
        {
            List<tbSpecialty> result = await DoctorApiRequestHelper.getSpecialityList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> _PatientForm(string FormType, int ID, int doctorid = 0, string doctorname = null, DateTime? appdate = null, DateTime? fromtime = null, DateTime? totime = null,int scheduleid=0)
        {
            ViewBag.doctorid = doctorid;
            ViewBag.doctorname = doctorname;
            ViewBag.appdate = appdate;
            ViewBag.fromtime = fromtime;
            ViewBag.totime = totime;
            ViewBag.scheduleid = scheduleid;
            tbPatient patient = new tbPatient();
            if (FormType == "Add")
            {
                return PartialView("_PatientForm", patient);
            }
            else
            {
                //tbPatient result = await BookingApiRequestHelper.GetPatientByID(ID);
                //return PartialView("_PatientForm", result);
                return null;
            }
        }

        public async Task<ActionResult> UpsertPatient(tbPatient patient, int doctorid = 0, string doctorname = null, DateTime? appdate = null, DateTime? fromtime = null, DateTime? totime = null, int scheduleid = 0)
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
            shvm.appointment.HospitalId = CareMeClient.Helper.CookieHelper.getstaffHospitalID();
            //      shvm.appointment.HospitalName = CareMeClient.Helper.CookieHelper.getstaffHospitalName();

            shvm.patient = patient;
            PatientAppointmentViewModel result = await BookingApiRequestHelper.UpsertPatient(shvm);
            if (result != null)
            {
                if(result.patient != null)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> _scheduleListbyDoctorID(int doctorid = 0, string docname = null, DateTime? date=null)
        {
            int hospitalid = Helper.CookieHelper.getstaffHospitalID();
            List<tbScheduleData> result = await BookingApiRequestHelper.scheduleListbyDoctorID(doctorid, docname, hospitalid, date);
            ViewBag.docid = doctorid;
            ViewBag.docname = docname;
            if(date != null)
            {
                ViewBag.Date = date.Value.Date;
            }
            else{
                ViewBag.Date = MyExtension.getLocalTime(DateTime.UtcNow);
            }
            
            return PartialView("_scheduleListbyDoctorID", result);
        }


    }
}