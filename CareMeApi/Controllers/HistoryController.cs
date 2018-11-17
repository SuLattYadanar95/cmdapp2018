using CareMeApi.Repository;
using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeApi.Controllers
{
    public class HistoryController : ApiController
    {
        CaremeDBContext dbContext;
        private AppointmentRepository appointmentRepo = null;

        public HistoryController()
        {
            dbContext = new CaremeDBContext();
            appointmentRepo = new AppointmentRepository(dbContext);
        }


        [Route("api/appointment/historylist")]
        [HttpGet]
        public HttpResponseMessage historylist(HttpRequestMessage request, string name = null, DateTime? datetime = null, int pagesize = 10, int page = 1, int hospitalid = 0)
        {
            Expression<Func<tbAppointment, bool>> namefilter, datetimefilter = null;


            if (name != null)
            {
                namefilter = l => l.PatientName.Contains(name);
            }
            else
            {
                namefilter = l => l.IsDeleted != true;
            }

            IQueryable<tbAppointment> result = null;
            if (datetime != null)
            {
                result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != null && a.Status != "BOOKED" && a.AppointmentDateTime.Value.Day == datetime.Value.Day && a.AppointmentDateTime.Value.Month == datetime.Value.Month && a.HospitalId == hospitalid).Where(namefilter);
            }
            else
            {
                result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != null && a.Status != "BOOKED" && a.HospitalId == hospitalid).Where(namefilter);
            }
            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.AppointmentDateTime)
                         .Skip(pagesize * (page - 1)).Take(pagesize);
            PagedListServer<tbAppointment> model = new PagedListServer<tbAppointment>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<tbAppointment>>(HttpStatusCode.OK, model);
        }

    }
}
