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
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> _historylist(string name = null, DateTime? datetime = null, int pagesize = 10, int page = 1)
        {
            int hospitalid = CookieHelper.getstaffHospitalID();
            PagedListClient<tbAppointment> result = await HistoryApiRequestHelper.GetAppointmentHistoryListWithPaging(name, datetime, pagesize, page, hospitalid);
            return PartialView("_historylist", result);
        }
    }
}