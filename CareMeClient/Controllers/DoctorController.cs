//using Data.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Infra.Helper;
//using Data.Models;

//namespace CareMeClient.Controllers
//{
//    public class DoctorController : Controller
//    {
//        // GET: Doctor
//        public ActionResult Index()
//        {
//            return View();
//        }
//        //public async Task<ActionResult> _doctorlist(string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1)
//        //{
//        //    PagedListClient<DoctorHospitalViewModel> result = await DoctorApiRequestHelper.GetDoctorWithPaging(doctorname, hospitalname, pagesize, page);
//        //    return PartialView("_doctorList", result);
//        //}

//        public async Task<ActionResult> _doctorlist(string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1)
//        {
//            PagedListClient<tbDoctor> result = await DoctorApiRequestHelper.GetDoctorWithPaging(doctorname, hospitalname, pagesize, page);
//            return PartialView("_doctorList", result);
//        }

//        public async Task<ActionResult> _DoctorForm(string FormType, int ID)
//        {
//            tbDoctor doctor = new tbDoctor();
//            if (FormType == "Add")
//            {
//                return PartialView("_doctorForm", doctor);
//            }
//            else
//            {
//                tbDoctor result = await DoctorApiRequestHelper.GetDoctorById(ID);
//                //string url = string.Format("api/team1Category/GetCategoryById?ID={0}", ID);
//                //tbCategory result = await ApiRequest<tbCategory>.GetRequest(url);
//                return PartialView("_doctorForm", result);
//            }
//        }

//        public async Task<ActionResult> UpSertDoctor(tbDoctor doctor)
//        {
//            tbDoctor result = await DoctorApiRequestHelper.UpSertDoctor(doctor);
//            //var url = "api/team1Category/create";
//            //tbCategory result = await ApiRequest<tbCategory>.PostRequest(url, category);
//            if (result != null)
//            {
//                return Json("Success", JsonRequestBehavior.AllowGet);
//            }
//            else
//            {
//                return Json("Fail", JsonRequestBehavior.AllowGet);
//            }
//        }


//        public async Task<ActionResult> DeleteDoctor(int ID)
//        {
//            tbDoctor result = await DoctorApiRequestHelper.DeleteDoctor(ID);
//            if (result != null)
//            {
//                return Json("Success", JsonRequestBehavior.AllowGet);
//            }
//            else
//            {
//                return Json("Fail", JsonRequestBehavior.AllowGet);
//            }
//        }

//    }
//}