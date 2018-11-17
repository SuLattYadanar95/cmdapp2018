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
    public class AppointmentController : Controller
    {
        // GET: Appointment
  
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public async Task<ActionResult> _list(string name = null,string status = null, int pagesize = 10, int page = 1,string datetime=null)
        //{
        //    PagedListClient<tbAppointment> result = await AppointmentApiRequestHelper.GetAppointmentWithPaging(name, status, pagesize, page, datetime);
        //    return PartialView("_appointmentList", result);
        //}

        //public async Task<ActionResult> _statusChange(int id , string statuschange = null)
        //{
        //     tbAppointment result = await AppointmentApiRequestHelper.StatusChange(id,statuschange);
        //    return PartialView("_appointmentList", result);
        //}
        //////public async Task<ActionResult> _waitinglist(string name = null, string status = null, int pagesize = 10, int page = 1)
        //////{
        //////    PagedListClient<tbAppointment> result = await AppointmentApiRequestHelper.GetAppointmentWaitingListWithPaging(name, status, pagesize, page);
        //////    return PartialView("_waitinglist", result);
        //////}

     
    }
}