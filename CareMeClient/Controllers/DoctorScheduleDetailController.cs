using CareMeClient.Helper;
using Data.Models;
using Data.ViewModels;
using Extensions;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CareMeClient.Controllers
{
    public class DoctorScheduleDetailController : Controller
    {
        // GET: DoctorScheduleDetail
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> _list(int id = 0, string docname = null, DateTime? fromdate = null, DateTime? todate = null,int pagesize=10,int page=1)
        {
            int hospitalid = CareMeClient.Helper.CookieHelper.getstaffHospitalID();
            if (fromdate != null && todate != null)
            {
                ViewBag.appDate = MyExtension.getLocalTime(DateTime.UtcNow);
                PagedListClient<ScheduleDoctorViewModel> result = await ScheduleApiRequestHelper.list(id, docname, fromdate, todate, pagesize, page,hospitalid);
                return PartialView("_searchList", result);
            }
            else
            {
                ViewBag.appDate = MyExtension.getLocalTime(DateTime.UtcNow);
                PagedListClient<ScheduleDoctorViewModel> result = await ScheduleApiRequestHelper.list(id, docname, fromdate, todate, pagesize, page,hospitalid);
                return PartialView("_DoctorScheduleList", result);
            }
              
         
          
        }

        public async Task<ActionResult> _nextschedule(int docid = 0, int hospitalid = 0, DateTime? appDate = null, string Type = null)
        {
            hospitalid = CareMeClient.Helper.CookieHelper.getstaffHospitalID();
            ViewBag.Type = Type;
            if (Type == "next")
            {
                ViewBag.appDate = appDate.Value.AddDays(7);
                ViewBag.doctorid = docid;
                ViewBag.hospitalid = hospitalid;
            }
            else if (Type == "prev")
            {
                ViewBag.appDate = appDate.Value.AddDays(-7);
                ViewBag.doctorid = docid;
                ViewBag.hospitalid = hospitalid;
            }
          
            List<tbScheduleData> result = await ScheduleApiRequestHelper.schedulebydoctorid(docid, hospitalid, appDate, Type);
            return PartialView("_nextschedulelist", result);
        }



        public async Task<ActionResult> _GenerateScheduleForm(string FormType, int ID)
        {
            ViewBag.hospitalid = CareMeClient.Helper.CookieHelper.getstaffHospitalID();
            ViewBag.hospitalname = CareMeClient.Helper.CookieHelper.getstaffHospitalName();
            if (FormType == "Add")
            {
                return PartialView("_GenerateScheduleForm");
            }
            else
            {
                return null;
            }
        }


        public async Task<ActionResult> UpSertGenerateSchedule(DateTime? fromdatepicker = null, DateTime? todatepicker = null, int doctorid = 0)
        {
            int hospitalid = CookieHelper.getstaffHospitalID();
            string hospitalname = CookieHelper.getstaffHospitalName();

            ViewBag.hospitalid = hospitalid;// CareMeClient.Helper.CookieHelper.getstaffHospitalID();
            ViewBag.hospitalname = hospitalname;// CareMeClient.Helper.CookieHelper.getstaffHospitalName();
           
            tbScheduleData result = await DoctorApiRequestHelper.UpSertGenerateSchedule(fromdatepicker, todatepicker, doctorid, hospitalid, hospitalname);

            if (result == null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> getgenerateschedulelist(int id = 0)
        {

            List<tbSchedule> result = await DoctorApiRequestHelper.getgenerateschedulelist(id);
            return PartialView("_docScheduleTemplate", result);
            // return Json(result,JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> getdoctorlistbyhospital(int hospitalID = 0)
        {
            hospitalID = CookieHelper.getstaffHospitalID();
            List<tbDoctorHospital> result = await DoctorApiRequestHelper.getdoctorlistbyhospital(hospitalID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> _ScheduleForm(string FormType, int ID, string docname = null, string hosname = null, int docid = 0, int hosid = 0)
        {
            tbScheduleData schedule = new tbScheduleData();
            schedule.DoctorName = docname;
            schedule.HospitalName = hosname;
            schedule.DoctorID = docid;
            schedule.HospitalID = hosid;
            if (FormType == "Add")
            {
                return PartialView("_ScheduleForm", schedule);
            }
            else
            {
                tbSchedule result = await DoctorApiRequestHelper.GetScheduleByID(ID);
                return PartialView("_ScheduleForm", result);
            }
        }

      

        [HttpPost]
        public async Task<ActionResult> UpSertSchedule(tbScheduleData schedule)
        {

            tbScheduleData result = await DoctorApiRequestHelper.UpSertSchedule(schedule);

            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> scheduledetaildelete(int ID = 0)
        {

            tbScheduleData result = await DoctorApiRequestHelper.scheduledetaildelete(ID);
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