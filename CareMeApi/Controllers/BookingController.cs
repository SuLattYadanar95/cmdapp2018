using CareMeApi.Repository;
using Data.Models;
using Data.ViewModels;
using Extensions;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeApi.Controllers
{
    public class BookingController : ApiController
    {
        CaremeDBContext dbContext;
        private DoctorRepository doctorRepo = null;
        private ScheduleDataRepository scheduledataRepo = null;
        public BookingController()
        {
            dbContext = new CaremeDBContext();
            doctorRepo = new DoctorRepository(dbContext);
            scheduledataRepo = new ScheduleDataRepository(dbContext);
        }



        //Get Doctor List by From Time
        [Route("api/Booking/DoctorListByTime")]
        [HttpGet]
        public HttpResponseMessage DoctorListByTime(HttpRequestMessage request, DateTime? fromtime = null,
            DateTime? totime = null, int pagesize = 0, int page = 0,int hospitalid=0, int appointmentid=0,DateTime? date= null)
        {

            // DoctorScheduleDataViewModel dsdvm = new DoctorScheduleDataViewModel();
            //List<tbScheduleData> schdata = new List<tbScheduleData>();

            //schdata = scheduledataRepo.Get().Where(a => a.IsDeleted != true).DistinctBy(a => a.Fromtime.Value.TimeOfDay).Where(a => a.Fromtime.Value.TimeOfDay == fromtime.Value.TimeOfDay).ToList();
            // List<tbDoctor> doctors = doctorRepo.Get().Where(a => a.IsDeleted != true).ToList();

            //var result = (from doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true)
            //              join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true && a.ID == hospitalid) on doc. equals h.ID
            //              join d in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true) on doc.DoctorID equals d.ID
            //              join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.Fromtime == fromtime && a.HospitalID == hospitalid)
            //              on doc.ID equals sch.DoctorID
            //              select doc).DistinctBy(a=>a.ID).ToList();
            Expression<Func<tbScheduleData, bool>> appointmentFilter;
            if(appointmentid > 0)
            {
                appointmentFilter = a => a.ID == appointmentid;
            }
            else
            {
                appointmentFilter = a => a.IsDeleted != true;
            }
            Expression<Func<tbScheduleData, bool>> appdateFilter;
            if (date != null)
            {
                date = date.Value.Date;
                var nextdate = date.Value.AddDays(1);
                appdateFilter = a => a.AppointmentDatetime >= date && a.AppointmentDatetime <= nextdate;
            }
            else
            {
                appdateFilter = a => a.IsDeleted != true;
            }

            var result = (from doc in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true)
                          //join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true && a.ID == hospitalid) on doc.HospitalID equals h.ID
                          join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid && a.Fromtime == fromtime && a.Totime == totime)
                          .Where(appdateFilter)
                          on doc.DoctorID equals sch.DoctorID
                          join d in dbContext.tbDoctors.Where(a => a.IsDeleted != true) on sch.DoctorID equals d.ID
                          select d).DistinctBy(a => a.ID).ToList();

            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.Name).Skip(pagesize * (page - 1)).Take(pagesize);

            PagedListServer<tbDoctor> model = new PagedListServer<tbDoctor>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<tbDoctor>>(HttpStatusCode.OK, model);
        }


        //Get Appointment Schedule data by date time and clicking Carosel 
        [Route("api/Booking/AppointmentSchedule")]
        [HttpGet]
        public HttpResponseMessage AppointmentSchedule(HttpRequestMessage request, DateTime? datetime, string type = null,int hospitalid=0)
        {
            AppointScheduleViewModel asvm = new AppointScheduleViewModel();
            List<tbScheduleData> result = null;
            if (type == null)
            {
                result = scheduledataRepo.Get().Where(a => a.IsDeleted != true & a.AppointmentDatetime.Value.Day == datetime.Value.Day && a.HospitalID == hospitalid).ToList();

                asvm.appointDatetime = datetime;
                asvm.scheduledatas = result;
            }
            else if (type == "next")
            {
                DateTime nextDate = datetime.Value.AddDays(1);
                result = scheduledataRepo.Get().Where(a => a.IsDeleted != true & a.AppointmentDatetime.Value.Day == nextDate.Day && a.HospitalID == hospitalid).ToList();

                asvm.appointDatetime = nextDate;
                asvm.scheduledatas = result;
            }
            else
            {
                DateTime prevDate = datetime.Value.AddDays(-1);
                result = scheduledataRepo.Get().Where(a => a.IsDeleted != true & a.AppointmentDatetime.Value.Day == prevDate.Day && a.HospitalID == hospitalid).ToList();

                asvm.appointDatetime = prevDate;
                asvm.scheduledatas = result;
            }

            return request.CreateResponse<AppointScheduleViewModel>(HttpStatusCode.OK, asvm);
        }

        //Get Doctor List by Today Date
        [Route("api/Booking/ListByDate")]
        [HttpGet]

        public HttpResponseMessage ListByDate(HttpRequestMessage request, DateTime? datetime = null, int pagesize = 5, int page = 1,int hospitalid=0)
        {



            //var result = (from doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true)
            //              join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.AppointmentDatetime.Value.Day == datetime.Value.Day && a.HospitalID == hospitalid)
            //              on doc.ID equals sch.DoctorID
            //              select new ScheduleDoctorViewModel
            //              {
            //                  doctor = doc,
            //                  schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doc.ID).ToList(),

            //              }).DistinctBy(a => a.doctor.ID).ToList();

            var result = (from doc in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true)
                          join d in dbContext.tbDoctors.Where(a => a.IsDeleted != true) on doc.DoctorID equals d.ID
                          join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true) on doc.HospitalID equals h.ID
                          join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.AppointmentDatetime.Value.Day == datetime.Value.Day && a.HospitalID == hospitalid)
                          on doc.DoctorID equals sch.DoctorID
                          select new ScheduleDoctorViewModel {
                              doctor = d,
                             schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doc.ID).ToList(),

                          }).DistinctBy(a => a.doctor.ID).ToList();




            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.doctor.Name).Skip(pagesize * (page - 1)).Take(pagesize);

            PagedListServer<ScheduleDoctorViewModel> model = new PagedListServer<ScheduleDoctorViewModel>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();

            return request.CreateResponse<PagedListServer<ScheduleDoctorViewModel>>(HttpStatusCode.OK, model);
        }



        //Get Doctor List By ScheduleData
        [Route("api/Booking/list")]
        [HttpGet]
        public HttpResponseMessage list(HttpRequestMessage request, int docid = 0, int specialityid = 0, string docname = null, DateTime? datetime = null, DateTime? fromtime = null, DateTime? totime = null, int pagesize = 5, int page = 1,int hospitalid=0)
        {

            Expression<Func<tbScheduleData, bool>>   datetimefilter, fromtimefilter, totimefilter = null;
            Expression<Func<tbDoctor, bool>> specialityidfilter, docnamefilter, idfilter = null;
            if (docid > 0)
            {
                idfilter = l => l.ID == docid;
            }
            else
            {
                idfilter = l => l.IsDeleted != true;
            }

            if (specialityid > 0)
            {
                specialityidfilter = l => l.SpecialityID == specialityid;
            }
            else
            {
                specialityidfilter = l => l.IsDeleted != true;
            }

            if (docname != null)
            {
                docnamefilter = l => l.Name.Contains(docname);
            }
            else
            {
                docnamefilter = l => l.IsDeleted != true;
            }

            if (datetime != null)
            {
                datetimefilter = l => l.AppointmentDatetime == datetime;
            }
            else
            {
                datetimefilter = l => l.IsDeleted != true;
            }

            if (fromtime != null)
            {
                fromtimefilter = l => l.Fromtime == fromtime;
            }
            else
            {
                fromtimefilter = l => l.IsDeleted != true;
            }
            if (totime != null)
            {
                totimefilter = l => l.Totime == totime;
            }
            else
            {
                totimefilter = l => l.IsDeleted != true;
            }
            List<ScheduleBookingViewModel> result = null;
            if (datetime == null)
            {

                var today = MyExtension.getLocalTime(DateTime.UtcNow).Date;

                //var result = (from doc in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true)
                //              join d in dbContext.tbDoctors.Where(a => a.IsDeleted != true) on doc.DoctorID equals d.ID
                //              join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true && a.ID == hospitalid) on doc.DoctorID equals h.ID
                //              join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.AppointmentDatetime.Value.Day == datetime.Value.Day && a.HospitalID == hospitalid)
                //              on doc.ID equals sch.DoctorID
                //              select new ScheduleDoctorViewModel
                //              {
                //                  doctor = d,
                //                  schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doc.ID).ToList(),

                //              }).DistinctBy(a => a.doctor.ID).ToList();




                result = (from doh in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true)
                         
                         // join h in dbContext.tbHospitals.Where(a=>a.IsDeleted != true) on doh.HospitalID equals h.ID
                          join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid)
                          on doh.DoctorID equals sch.DoctorID
                          join doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true)
                         .Where(idfilter).Where(specialityidfilter).Where(docnamefilter) on sch.DoctorID equals doc.ID
                          select new ScheduleBookingViewModel
                          {
                              doctor = doc,
                              schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doc.ID && a.HospitalID==hospitalid).ToList(),

                          }).DistinctBy(a => a.doctor.ID).ToList();



            }

            else
            {
                result = (from doh in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true)
                          
                          //join h in dbContext.tbHospitals.Where(a => a.IsDeleted != true) on doh.HospitalID equals h.ID
                          join sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.AppointmentDatetime.Value.Day == datetime.Value.Day)
                          on doh.DoctorID equals sch.DoctorID
                          join doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true)
                          .Where(idfilter).Where(specialityidfilter).Where(docnamefilter) on doh.DoctorID equals doc.ID
                          select new ScheduleBookingViewModel
                          {
                              doctor = doc,
                              schedule = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doc.ID).ToList(),

                          }).DistinctBy(a => a.doctor.ID).ToList();

            }


            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.doctor.Name).Skip(pagesize * (page - 1)).Take(pagesize);
            PagedListServer<ScheduleBookingViewModel> model = new PagedListServer<ScheduleBookingViewModel>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();


            return request.CreateResponse<PagedListServer<ScheduleBookingViewModel>>(HttpStatusCode.OK, model);
        }

        //Schedule List By Doctor ID
        [Route("api/Booking/scheduleListbyDoctorID")]
        [HttpGet]
        public HttpResponseMessage scheduleListbyDoctorID(HttpRequestMessage request, int doctorid = 0, string docname = null, int hospitalid = 0, DateTime? date = null)
        {
            if(date != null)
            {
                date = date.Value.Date;
            }
            else
            {
                date = MyExtension.getLocalTime(DateTime.UtcNow).Date;
            }
           
            var result = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doctorid && a.HospitalID == hospitalid && a.AppointmentDatetime >= date).ToList();
            return request.CreateResponse<List<tbScheduleData>>(HttpStatusCode.OK, result);
        }


        [Route("api/Booking/getAppointments")]
        [HttpGet]
        public HttpResponseMessage getAppointments(HttpRequestMessage request,string userid = null)
        {
            tbPatient patient = dbContext.tbPatients.Where(a => a.MsgrID == userid).FirstOrDefault();
            List<tbAppointment> applist = new List<tbAppointment>();
            if (patient != null)
            {
              applist = dbContext.tbAppointments.Where(a => a.IsDeleted != true && a.PatientId == patient.ID).ToList();

            }
            else
            {
                applist = dbContext.tbAppointments.Where(a => a.IsDeleted != true).ToList();
            }
            return request.CreateResponse<List<tbAppointment>>(HttpStatusCode.OK, applist);
        }

        //getAppointments

        #region Careme WEb





        #endregion
    }
}
