using CareMeClient.Helper;
using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CareMeClient.Controllers
{
    public class DoctorScheduleController : Controller
    {
        // GET: DoctorSchedule
        public async Task<ActionResult> Index()
        {
            DoctorSpecialitiesViewModel result = await DoctorApiRequestHelper.GetDocSpecialtyListFilter();
            return View(result);
        }

        public async Task<ActionResult> _doctorScheduleList(string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1,int doctorid=0,int specialtyid=0,int hospitalid=0)
        {
            string specialties = null;
            hospitalid = CookieHelper.getstaffHospitalID();
            PagedListClient<DoctorHospitalViewModel> result = await DoctorApiRequestHelper.GetDoctorWithPaging(doctorname, hospitalname, pagesize, page, doctorid, specialtyid, specialties,hospitalid);
            return PartialView("_doctorScheduleList", result);
        }

        public async Task<ActionResult> getSpecialityList()
        {
            List<tbSpecialty> result = await DoctorApiRequestHelper.getSpecialityList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

      

        public async Task<ActionResult> _ScheduleForm2(string FormType, int ID, string docname = null, string hosname = null, int docid = 0, int hosid = 0)
        {
            tbSchedule schedule = new tbSchedule();
            schedule.DoctorName = docname;
            schedule.HospitalName = hosname;
            schedule.DoctorID = docid;
            schedule.HospitalID = hosid;
            if (FormType == "Add")
            {
                return PartialView("_ScheduleForm2", schedule);
            }
            else
            {
                tbSchedule result = await DoctorApiRequestHelper.GetScheduleByID(ID);
                return PartialView("_ScheduleForm2", result);
            }
        }


        public async Task<ActionResult> UpSertSchedule2(tbSchedule schedule)
        {
          //  tbSchedule schedule = new tbSchedule();
            //schedule.DoctorID = doctorid;
            //schedule.DoctorName = doctorname;
            //schedule.HospitalID = hospitalid;
            //schedule.HospitalName = hospitalname;
            //schedule.Fromtime = timepicker1;
            //schedule.Totime = timepicker2;
            tbSchedule result = await DoctorApiRequestHelper.UpSertSchedule2(schedule);

            if (result != null)
            {
                //  return Json(result, JsonRequestBehavior.AllowGet);
                return PartialView("_schedule", result);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> _DoctorForm(string FormType, int ID)
        {
            DoctorSpecialityViewModel dsvm = new DoctorSpecialityViewModel();
            dsvm.doctor = new tbDoctor();
            dsvm.specialty = new tbSpecialty();
            if (FormType == "Add")
            {
                return PartialView("_doctorForm", dsvm);
            }
            else
            {

                DoctorSpecialityViewModel result = await DoctorApiRequestHelper.GetDoctorById(ID);
                result.hospitalid = CookieHelper.getstaffHospitalID();
                result.hospitalname = CookieHelper.getstaffHospitalName();
                return PartialView("_doctorForm", result);
               // return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpsertDoctor(DoctorSpecialityViewModel dhvm)
        {

            dhvm.hospitalid = CookieHelper.getstaffHospitalID();
            dhvm.hospitalname = CookieHelper.getstaffHospitalName();
            DoctorSpecialityViewModel result = await DoctorApiRequestHelper.UpSertDoctor(dhvm);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<ActionResult> _scheduleListbyDoctorID(int doctorid = 0,string doctorname=null,int hospitalid=0,string hospitalname=null)
        {
           
            List<tbSchedule> result = await DoctorApiRequestHelper.scheduleListbyDoctorID(doctorid);
            ViewBag.hosname = hospitalname;
            ViewBag.hosid = hospitalid;
            ViewBag.docid = doctorid;
            ViewBag.docname = doctorname;
            return PartialView("_scheduleListbyDoctor", result);
        }

      


        public async Task<ActionResult> doctordelete(int ID = 0)
        {
            int hospitalID = CookieHelper.getstaffHospitalID();
            DoctorHospitalViewModel result = await DoctorApiRequestHelper.doctordelete(ID, hospitalID);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> scheduledelete(int ID = 0)
        {

            tbSchedule result = await DoctorApiRequestHelper.scheduledelete(ID);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        //UpSertGenerateSchedule

    }
}