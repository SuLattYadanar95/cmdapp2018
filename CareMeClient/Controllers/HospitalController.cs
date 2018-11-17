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
    public class HospitalController : Controller
    {
        // GET: Hospital
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> _hospitalList(string hospitalname = null, int pagesize = 10, int page = 1)
        {
            PagedListClient<tbHospital> result = await HospitalApiRequestHelper.GetHospitalWithPaging(hospitalname, pagesize, page);
            return PartialView("_hospitalList", result);
        }

        public async Task<ActionResult> _HospitalForm(string FormType, int ID)
        {
            tbHospital hospital = new tbHospital();
            if (FormType == "Add")
            {
                return PartialView("_hospitalForm", hospital);
            }
            else
            {
                tbHospital result = await HospitalApiRequestHelper.GetHospitalById(ID);
                return PartialView("_hospitalForm", result);
              
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpSertHospital(tbHospital hospital)
        {
            tbHospital result = await HospitalApiRequestHelper.UpSertHospital(hospital);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<ActionResult> hospitaldelete(int ID = 0)
        {

            tbHospital result = await HospitalApiRequestHelper.hospitaldelete(ID);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<ActionResult> GetState()
        {
            List<tbLocation> result = await HospitalApiRequestHelper.getstate();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTownShip(string state = null)
        {
            IEnumerable<string> result = await HospitalApiRequestHelper.GetTownShip(state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}