using CareMeApi.Models;
using CareMeApi.Repository;
using Data.Models;
using Data.ViewModels;
using Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoreLinq;


namespace CareMeApi.Controllers
{
    public class ScheduleController : ApiController
    {
        CaremeDBContext dbContext;
        private ScheduleDataRepository scheduleRepo = null;
        private DoctorRepository doctorRepo = null;

        public ScheduleController()
        {
            dbContext = new CaremeDBContext();
            scheduleRepo = new ScheduleDataRepository(dbContext);
           // scheduleRepo = new ScheduleRepository(dbContext);
           doctorRepo = new DoctorRepository(dbContext);
        }

        [Route("api/schedule/list")]
        [HttpGet]
        public HttpResponseMessage list(HttpRequestMessage request, int id = 0, string docname = null, DateTime? fromdate = null, DateTime? todate = null, int pagesize = 10, int page = 1, int hospitalid = 0)
        {

            Expression<Func<tbScheduleData, bool>> idfilter, fromtimefilter, totimefilter = null;
            Expression<Func<tbDoctorHospital, bool>> dochospitalfilter = null;
            Expression<Func<tbDoctor, bool>> docfilter = null;

            Expression<Func<tbHospital, bool>> hospitalFilter = null;
            if(hospitalid > 0)
            {
                hospitalFilter = h => h.ID == hospitalid;
            }
            else
            {
                hospitalFilter = h => h.IsDeleted != true;
            }

            if (id > 0)
            {
                idfilter = l => l.ID == id;
            }
            else
            {
                idfilter = l => l.IsDeleted != true;
            }
            if (docname != null)
            {
                dochospitalfilter = l => l.DoctorName.Contains(docname);
            }
            else
            {
                dochospitalfilter = l => l.IsDeleted != true;
            }
            if (docname != null)
            {
                docfilter = l => l.Name.Contains(docname);
            }
            else
            {
                docfilter = l => l.IsDeleted != true;
            }
            //if (fromtime != null)
            //{
            //    fromtimefilter = l => l.Fromtime == fromtime;
            //}
            //else
            //{
            //    fromtimefilter=l=>l.IsDeleted!=true;
            //}
            //if (totime != null)
            //{
            //    totimefilter = l => l.Totime == totime;
            //}
            //else
            //{
            //    totimefilter = l => l.IsDeleted != true;
            //}

            var today = MyExtension.getLocalTime(DateTime.UtcNow).Date;
            var nextsevendayfromtoday = today.AddDays(7);
            nextsevendayfromtoday = nextsevendayfromtoday.Date;
            List<ScheduleDoctorViewModel> result = null;

            var schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true).Where(a => a.AppointmentDatetime >= fromdate && a.AppointmentDatetime <= todate).ToList();

            //var result1 = (from doc in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid)
            //               join h in dbContext.tbHospitals.Where(a=>a.IsDeleted!= true).Where(hospitalFilter) on doc.HospitalID equals h.ID
            //               join d in dbContext.tbHospitals.Where(a=>a.IsDeleted != true) on doc.DoctorID equals d.ID
            //               join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true).Where(a => a.AppointmentDatetime >= fromdate && a.AppointmentDatetime <= todate)
            //               on doc.DoctorID equals sch.DoctorID
            //               select sch).ToList();
            if (fromdate != null && todate != null)
            {
                fromdate = fromdate.Value.Date;
                todate = todate.Value.Date.AddDays(1);
                result = (from doc in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid)
                          join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true).Where(hospitalFilter) on doc.HospitalID equals h.ID
                          
                          join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true).Where(a => a.AppointmentDatetime >= fromdate && a.AppointmentDatetime <= todate)                        
                          on doc.DoctorID equals sch.DoctorID
                          join d in dbContext.tbDoctors.Where(a => a.IsDeleted != true).Where(docfilter) on sch.DoctorID equals d.ID
                          select new ScheduleDoctorViewModel
                          {
                              doctor = dbContext.tbDoctors.Where(a => a.IsDeleted != true && a.ID == d.ID).FirstOrDefault(),
                              dochos = doc,
                              schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == d.ID && a.HospitalID == doc.HospitalID).Where(a => a.AppointmentDatetime >= fromdate && a.AppointmentDatetime <= todate).ToList(),
                          }).ToList().DistinctBy(a => a.doctor.ID).ToList();
            }
            else
            {
                result = (from doc in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid).Where(dochospitalfilter)
                          join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true).Where(hospitalFilter) on doc.HospitalID equals h.ID
                          join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true)
                          on doc.DoctorID equals sch.DoctorID
                          join d in dbContext.tbDoctors.Where(a => a.IsDeleted != true).Where(docfilter) on sch.DoctorID equals d.ID
                          select new ScheduleDoctorViewModel
                          {
                              doctor = dbContext.tbDoctors.Where(a => a.IsDeleted != true && a.ID == d.ID).FirstOrDefault(),
                              dochos = doc,
                              schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doc.DoctorID && a.AppointmentDatetime >= today && a.AppointmentDatetime <= nextsevendayfromtoday).ToList()
                          }).DistinctBy(a=>a.doctor.ID).ToList();
            }


            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.doctor.Name)
                         .Skip(pagesize * (page - 1)).Take(pagesize);
            PagedListServer<ScheduleDoctorViewModel> model = new PagedListServer<ScheduleDoctorViewModel>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<ScheduleDoctorViewModel>>(HttpStatusCode.OK, model);

            // return request.CreateResponse<List<ScheduleDoctorViewModel>>(HttpStatusCode.OK, result); 
        }

        [Route("api/schedule/schedulebydoctorid")]
        [HttpGet]
        public HttpResponseMessage schedulebydoctorid(HttpRequestMessage request, int docid = 0, int hospitalid = 0, DateTime? appDate = null, string Type = null)
        {

            List<tbScheduleData> result = null;
            if (Type == null)
            {
                var today = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                var nextsevenday = today.AddDays(7);
                result = scheduleRepo.GetWithoutTracking().Where(a => a.DoctorID == docid && a.HospitalID == hospitalid && a.IsDeleted != true && a.AppointmentDatetime >= today && a.AppointmentDatetime <= nextsevenday).ToList();
            }
            else if (Type == "next")
            {
                DateTime nextDate = appDate.Value.AddDays(7);
                DateTime nextSevenDate = nextDate.AddDays(7);
                nextDate = nextDate.Date;
                nextSevenDate = nextSevenDate.Date;

                result = scheduleRepo.GetWithoutTracking().Where(a => a.DoctorID == docid && a.HospitalID == hospitalid && a.IsDeleted != true && a.AppointmentDatetime >= nextDate && a.AppointmentDatetime <= nextSevenDate).ToList();
            }
            else if (Type == "prev")
            {

                DateTime prevDate = appDate.Value.Date;
                DateTime prevSevenDate = prevDate.AddDays(-7);
                prevSevenDate = prevSevenDate.Date;

                result = scheduleRepo.GetWithoutTracking().Where(a => a.DoctorID == docid && a.HospitalID == hospitalid && a.IsDeleted != true && a.AppointmentDatetime >= prevSevenDate && a.AppointmentDatetime <= prevDate).ToList();
            }
            return request.CreateResponse<List<tbScheduleData>>(HttpStatusCode.OK, result);
        }





        [HttpGet]
        [Route("api/Schedule/ScheduleDetailDelete")]
        public HttpResponseMessage ScheduleDelete(HttpRequestMessage request, int ID)
        {
            tbScheduleData UpdatedEntity = new tbScheduleData();

            tbScheduleData schedule = scheduleRepo.Get().Where(a => a.ID == ID).FirstOrDefault();

            schedule.IsDeleted = true;

            UpdatedEntity = scheduleRepo.UpdatewithObj(schedule);


            return request.CreateResponse<tbScheduleData>(HttpStatusCode.OK, UpdatedEntity);
        }


        [HttpGet]
        [Route("api/Schedule/getScheduleDetail")]
        public HttpResponseMessage getScheduleDetail(HttpRequestMessage request, int ID)
        {
            tbScheduleData schedule = scheduleRepo.Get().Where(a => a.ID == ID && a.IsDeleted != true).FirstOrDefault();
            
            return request.CreateResponse<tbScheduleData>(HttpStatusCode.OK, schedule);
        }


    }
}


          