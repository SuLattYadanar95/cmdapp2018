using CareMeMobileApi.IService;
using CareMeMobileApi.Repository;
using CareMeMobileApi.ViewModels;
using Data.Models;
using Data.ViewModels;
using Extensions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeMobileApi.Controllers
{
    public class HomeController : ApiController
    {
        CaremeDBContext dbContext;

        private ScheduleRepository scheduleRepo = null;
        private HospitalRepository hospitalRepo = null;
        private DoctorRepository doctorRepo = null;
        private AppointmentRepository appointmentRepo = null;
        IScheduleService iScheduleService;


        public HomeController(IScheduleService iScheduleService)
        {
            this.iScheduleService = iScheduleService;
            dbContext = new CaremeDBContext();
            scheduleRepo = new ScheduleRepository(dbContext);
            hospitalRepo = new HospitalRepository(dbContext);
            doctorRepo = new DoctorRepository(dbContext);
            appointmentRepo = new AppointmentRepository(dbContext);
        }

        [HttpGet, Route("api/home/getData")]
        public HttpResponseMessage getData(HttpRequestMessage request, int doctorid = 0)
        {
            DoctorHomeViewModel homeObj = new DoctorHomeViewModel();
            homeObj.doctorid = doctorid;
            homeObj.doctorName = doctorRepo.GetWithoutTracking().Where(a => a.ID == doctorid).Select(a => a.Name).FirstOrDefault();
            DateTime localNow = MyExtension.getLocalTime(DateTime.UtcNow);
            using (CaremeDBContext context = new CaremeDBContext())
            {
                string timeFrame_Today = "Today";
                DateTime today = MyExtension.getLocalTime(DateTime.UtcNow);
                CountInTimeFrame today_Count = iScheduleService.hospitalSchedulesWithPatient(today, timeFrame_Today, doctorid);
             
                string timeFrame_Tomorrow = "Tomorrow";
                DateTime tomorrow = today.AddDays(1);
                CountInTimeFrame tomorrow_Count = iScheduleService.hospitalSchedulesWithPatient(tomorrow, timeFrame_Tomorrow, doctorid);

                DateTime DFtomorrow = today.AddDays(2);
                string timeFrame_DFTomorrow = DFtomorrow.Day + " " + DFtomorrow.ToString("MMM"); 
                CountInTimeFrame DFtomorrow_Count = iScheduleService.hospitalSchedulesWithPatient(DFtomorrow, timeFrame_DFTomorrow, doctorid);

                DateTime TDFtomorrow = today.AddDays(3);
                string timeFrame_TDFTomorrow = TDFtomorrow.Day + " " + TDFtomorrow.ToString("MMM");
                CountInTimeFrame TDFtomorrow_Count = iScheduleService.hospitalSchedulesWithPatient(TDFtomorrow, timeFrame_TDFTomorrow, doctorid);


                List<CountInTimeFrame> countInTimeframes = new List<CountInTimeFrame>();
                countInTimeframes.Add(today_Count);
                countInTimeframes.Add(tomorrow_Count);
                countInTimeframes.Add(DFtomorrow_Count);
                countInTimeframes.Add(TDFtomorrow_Count);
                homeObj.CountList = countInTimeframes;

                return request.CreateResponse<DoctorHomeViewModel>(HttpStatusCode.OK, homeObj);
            }

        }

    }

}

