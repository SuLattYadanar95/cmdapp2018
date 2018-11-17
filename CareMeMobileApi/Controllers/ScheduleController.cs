using CareMeMobileApi.IService;
using CareMeMobileApi.Repository;
using Data.Models;
using Data.ViewModels;
using Extensions;
using LinqKit;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeMobileApi.Controllers
{
    public class ScheduleController : ApiController
    {
        CaremeDBContext dbContext;
      
        private ScheduleRepository scheduleRepo = null;
        private HospitalRepository hospitalRepo = null;
        private ScheduleDataRepository scheduleDataRepo = null;
        IScheduleService iScheduleService;
        IScheduleDelService iScheduleDelService;

        public ScheduleController(IScheduleService iScheduleService, IScheduleDelService iScheduleDelService)
        {
            this.iScheduleDelService = iScheduleDelService;
            this.iScheduleService = iScheduleService;
            dbContext = new CaremeDBContext();
            scheduleRepo = new ScheduleRepository(dbContext);
            hospitalRepo = new HospitalRepository(dbContext);
            scheduleDataRepo = new ScheduleDataRepository(dbContext);
        }
         
        [Route("api/schedule/UpSertSchedule")]
        [HttpPost]
        public HttpResponseMessage Upsert(HttpRequestMessage request, tbSchedule schedule)
        {

            tbSchedule UpdatedEntity = null;
            if (schedule.ID > 0)
            {
                UpdatedEntity = scheduleRepo.UpdatewithObj(schedule);
            }
            else
            {
                schedule.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                schedule.IsDeleted = false;
                UpdatedEntity = scheduleRepo.AddWithGetObj(schedule);
            }
            return request.CreateResponse<tbSchedule>(HttpStatusCode.OK, UpdatedEntity);
        }

        [Route("api/schedule/scheduleDetail")]
        [HttpGet]
        public HttpResponseMessage scheduleDetail(HttpRequestMessage request, int scheduleID = 0)
        {
            tbSchedule result = scheduleRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(a => a.ID == scheduleID).FirstOrDefault();
          
            return request.CreateResponse<tbSchedule>(HttpStatusCode.OK, result);
        }

        [HttpGet, Route("api/schedule/scheduleList")]
        public HttpResponseMessage schedulelist(HttpRequestMessage request,int doctorid=0)
        {
            var result = from s in dbContext.tbSchedules.Where(a => a.IsDeleted != true && a.DoctorID == doctorid).DistinctBy(a=>a.HospitalID).ToList()
                         join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true)
                         on s.HospitalID equals h.ID
                         select new ScheduleHospitalViewModel
                         {
                             hospital = h,
                             schedules = dbContext.tbSchedules.Where(a => a.IsDeleted != true && a.DoctorID == doctorid && a.HospitalID == h.ID).ToList(),

                         };
            var res = result.ToList();

            return request.CreateResponse<List<ScheduleHospitalViewModel>>(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [Route("api/schedule/delete")]
        public HttpResponseMessage delete(HttpRequestMessage request, int scheduleid=0)
        {
            tbSchedule UpdatedSchedule = iScheduleDelService.deleteSchedulesByID(scheduleid);
            return request.CreateResponse<tbSchedule>(HttpStatusCode.OK, UpdatedSchedule);
        }

        [HttpGet]
        [Route("api/schedule/delSchedulesUnderHospital")]
        public HttpResponseMessage delSchedulesUnderHospital(HttpRequestMessage request, int doctorID, int hospitalID)
        {
            bool result = iScheduleDelService.deleteSchedulesUnderHospital(doctorID,hospitalID);
            return request.CreateResponse<bool>(HttpStatusCode.OK, result);
        }

        [HttpGet, Route("api/schedule/getNotiList")]
        public HttpResponseMessage getNotiList(HttpRequestMessage request, int doctorid = 0)

        {
            List<NotiTimeFrameViewModel> NotiResultList = new List<NotiTimeFrameViewModel>();


            DateTime TodayDate = MyExtension.getLocalTime(DateTime.UtcNow);
            NotiTimeFrameViewModel todayNotiObj = iScheduleService.scheduleNotis(TodayDate, doctorid, "Today");

            DateTime yesterdayDate = TodayDate.AddDays(-1);
            NotiTimeFrameViewModel yesterdayNotiObj = iScheduleService.scheduleNotis(yesterdayDate, doctorid, "Yesterday");
            if(todayNotiObj.Timeframe != null)
            {
                NotiResultList.Add(todayNotiObj);
            }
            if(yesterdayNotiObj.Timeframe != null)
            {
                NotiResultList.Add(yesterdayNotiObj);
            }

            return request.CreateResponse<List<NotiTimeFrameViewModel>>(HttpStatusCode.OK, NotiResultList);

        }

        [HttpGet, Route("api/schedule/stopAppointment")]
        public HttpResponseMessage stopAppointment(HttpRequestMessage request, int doctorid, int hospitalid, DateTime AppointmentDatetime)
        {
            tbScheduleData scheduleData = scheduleDataRepo.Get().Where(a => a.DoctorID == doctorid && a.HospitalID == hospitalid && a.AppointmentDatetime == AppointmentDatetime && a.IsDeleted != true).FirstOrDefault();

            scheduleData.MaxPatientCount = scheduleData.ReachedPatientCount;
            scheduleData.IsStopped = true;
            scheduleData = scheduleDataRepo.UpdatewithObj(scheduleData);

            if(scheduleData != null)
            {
                return request.CreateResponse<string>(HttpStatusCode.OK, "S001");
            }
            else
            {
                return request.CreateResponse<string>(HttpStatusCode.OK, "E001");
            }
        }

        [HttpGet, Route("api/schedule/limitAppointment")]
        public HttpResponseMessage limitAppointment(HttpRequestMessage request, int doctorid, int hospitalid , DateTime AppointmentDatetime, int toLimitQty, string Type = "2")
        {
            tbScheduleData scheduleData = scheduleDataRepo.Get().Where(a => a.DoctorID == doctorid && a.HospitalID == hospitalid && a.AppointmentDatetime == AppointmentDatetime && a.IsDeleted != true).FirstOrDefault();

            if (Type == "1")
            {
                scheduleData.MaxPatientCount = scheduleData.ReachedPatientCount + toLimitQty;
                scheduleData.IsLimited = true;
                scheduleData = scheduleDataRepo.UpdatewithObj(scheduleData);

            }
            else
            {
                scheduleData.MaxPatientCount = toLimitQty;
                scheduleData.IsLimited = true;
                scheduleData = scheduleDataRepo.UpdatewithObj(scheduleData);
            }
            if (scheduleData != null)
            {
                return request.CreateResponse<string>(HttpStatusCode.OK, "S001");
            }
            else
            {
                return request.CreateResponse<string>(HttpStatusCode.OK, "E001");
            }
        }



    }
}
