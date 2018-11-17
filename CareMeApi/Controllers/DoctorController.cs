using CareMeApi.IService;
using CareMeApi.Repository;
using Data.Helper;
using Data.Models;
using Data.ViewModels;
using Extensions;
using Infra.Helper;
using LinqKit;
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
    public class DoctorController : ApiController
    {
        CaremeDBContext dbContext;
        private DoctorRepository doctorRepo = null;
        private DoctorHospitalRepository doctorhospitalRepo = null;
        private ScheduleRepository scheduleRepo = null;
        private ScheduleDataRepository scheduledataRepo = null;
        
   
        private readonly IPhoto iPhoto;
        private readonly IQueryService iQueryService;
        public DoctorController(IPhoto iPhoto, IQueryService iQueryService)
        {
            this.iPhoto = iPhoto;
            this.iQueryService = iQueryService;
            dbContext = new CaremeDBContext();
            doctorRepo = new DoctorRepository(dbContext);
            doctorhospitalRepo = new DoctorHospitalRepository(dbContext);
            scheduleRepo = new ScheduleRepository(dbContext);
            scheduledataRepo = new ScheduleDataRepository(dbContext);
           
        }

        [Route("api/doctor/listOriginal")]
        [HttpGet]
        public HttpResponseMessage listOriginal(HttpRequestMessage request, string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1, int doctorid = 0, int specialtyid = 0, string specialties = null, int hospitalid = 0)
        {
            Expression<Func<tbDoctor, bool>> doctornamefilter, doctoridfilter, specialtyidfilter, specialtyfilter = null;
            if (doctorname != null)
            {
                doctornamefilter = l => l.Name.Contains(doctorname);
            }
            else
            {
                doctornamefilter = l => l.IsDeleted != true;
            }
            if (doctorid != 0)
            {
                doctoridfilter = l => l.ID == doctorid;
            }
            else
            {
                doctoridfilter = l => l.IsDeleted != true;
            }
            if (specialtyid != 0)
            {
                specialtyidfilter = l => l.SpecialityID == specialtyid;
            }
            else
            {
                specialtyidfilter = l => l.IsDeleted != true;
            }
            if (specialties != null)
            {
                specialtyfilter = PredicateBuilder.New<tbDoctor>();
                foreach (var specialty in specialties.Split('_'))
                {
                    specialtyfilter = specialtyfilter.Or(l => l.Specialty.Contains(specialty));
                }
            }
            else
            {
                specialtyfilter = l => l.IsDeleted != true;
            }
            // IQueryable<tbDoctor> result = doctorRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(doctornamefilter);

            IQueryable<DoctorHospitalViewModel> result = (from doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true).Where(doctornamefilter).Where(doctoridfilter).Where(specialtyidfilter).Where(specialtyfilter)
                                                          join dochos in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid)
                                                          on doc.ID equals dochos.DoctorID
                                                          select new DoctorHospitalViewModel
                                                          {
                                                              doctor = doc,
                                                              hospital = dochos,
                                                          }).DistinctBy(a => a.doctor.ID).AsQueryable(); //

            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.doctor.Accesstime)
                         .Skip(pagesize * (page - 1)).Take(pagesize);
            PagedListServer<DoctorHospitalViewModel> model = new PagedListServer<DoctorHospitalViewModel>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<DoctorHospitalViewModel>>(HttpStatusCode.OK, model);
        }


        [Route("api/doctor/list")]
        [HttpGet]
        public HttpResponseMessage List(HttpRequestMessage request, string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1,int doctorid = 0, int specialtyid = 0,string specialties = null, int hospitalid = 0)
        {
            Expression<Func<tbDoctor, bool>> doctornamefilter,doctoridfilter,specialtyidfilter, specialtyfilter = null;
            if (doctorname != null)
            {
                doctornamefilter = l => l.Name.Contains(doctorname);
            }
            else
            {
                doctornamefilter = l => l.IsDeleted != true;
            }
            if (doctorid != 0)
            {
                doctoridfilter = l => l.ID == doctorid;
            }
            else
            {
                doctoridfilter = l => l.IsDeleted != true;
            }
            if (specialtyid != 0)
            {
                specialtyidfilter = l => l.SpecialityID == specialtyid;
            }
            else
            {
                specialtyidfilter = l => l.IsDeleted != true;
            }
            if (specialties != null)
            {
                specialtyfilter = PredicateBuilder.New<tbDoctor>();
                foreach (var specialty in specialties.Split('_'))
                {
                    specialtyfilter = specialtyfilter.Or(l => l.Specialty.Contains(specialty));
                }
            }
            else
            {
                specialtyfilter = l => l.IsDeleted != true;
            }
            // IQueryable<tbDoctor> result = doctorRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(doctornamefilter);

            IQueryable<DoctorHospitalViewModel> result = (from doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true).Where(doctornamefilter).Where(doctoridfilter).Where(specialtyidfilter).Where(specialtyfilter)
                                           join dochos in dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid)
                                           on doc.ID equals dochos.DoctorID
                                           select new DoctorHospitalViewModel
                                           {
                                               doctor = doc,
                                               hospital = dochos,
                                           }).DistinctBy(a=>a.doctor.ID).AsQueryable(); //

            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.doctor.Accesstime)
                         .Skip(pagesize * (page - 1)).Take(pagesize);
            PagedListServer<DoctorHospitalViewModel> model = new PagedListServer<DoctorHospitalViewModel>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<DoctorHospitalViewModel>>(HttpStatusCode.OK, model);
        }
        [Route("api/doctor/get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1, int doctorid = 0, int specialtyid = 0, string specialties = null, int hospitalid = 0)
        {
            Expression<Func<tbDoctor, bool>> doctornamefilter, doctoridfilter, specialtyidfilter, specialtyfilter = null;
            Expression<Func<tbDoctorHospital, bool>> hospitalidfilter, hospitalnamefilter = null;
            if (doctorname != null)
            {
                doctornamefilter = l => l.Name.Contains(doctorname);
            }
            else
            {
                doctornamefilter = l => l.IsDeleted != true;
            }
            if (doctorid != 0)
            {
                doctoridfilter = l => l.ID == doctorid;
            }
            else
            {
                doctoridfilter = l => l.IsDeleted != true;
            }
            if (specialtyid != 0)
            {
                specialtyidfilter = l => l.SpecialityID == specialtyid;
            }
            else
            {
                specialtyidfilter = l => l.IsDeleted != true;
            }
            if (specialties != null)
            {
                specialtyfilter = PredicateBuilder.New<tbDoctor>();
                foreach (var specialty in specialties.Split('_'))
                {
                    specialtyfilter = specialtyfilter.Or(l => l.Specialty.ToLower().Contains(specialty.ToLower()));
                }
            }
            else
            {
                specialtyfilter = l => l.IsDeleted != true;
            }
            if (hospitalid != 0)
            {
                hospitalidfilter = l => l.HospitalID == hospitalid;
            }
            else
            {
                hospitalidfilter = l => l.IsDeleted != true;
            }
            if (hospitalname != null)
            {
                hospitalnamefilter = l => l.HospitalName.StartsWith(hospitalname);
            }
            else
            {
                hospitalnamefilter = l => l.IsDeleted != true;
            }
            // IQueryable<tbDoctor> result = doctorRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(doctornamefilter);

            IQueryable<tbDoctor> result = (from doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true).Where(doctornamefilter).Where(doctoridfilter).Where(specialtyidfilter).Where(specialtyfilter)
                                           join dochos in dbContext.tbDoctorHospitals.Where(hospitalidfilter).Where(hospitalnamefilter)
                                           on doc.ID equals dochos.DoctorID
                                           select doc).DistinctBy(a => a.ID).AsQueryable(); //
            var skipindex = pagesize * (page - 1);
            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);

            var model = new PagedListServer<tbDoctor>
            {
                TotalCount = totalCount,
                TotalPages = totalpages,
                Results = result.OrderBy(a => a.Name).Skip(skipindex).Take(pagesize).ToList()
            };
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<tbDoctor>>(HttpStatusCode.OK, model);
        }
        [Route("api/doctor/specialties")]
        [HttpGet]
        public HttpResponseMessage GetSpecialties(HttpRequestMessage request, int doctorid = 0, string doctorname = null, int hospitalid = 0, string hospitalname = null, int pagesize = 10, int page = 1)
        {
            List<string> result = null;
            Expression<Func<tbDoctor, bool>> doctornamefilter, doctoridfilter = null;
            Expression<Func<tbDoctorHospital, bool>> hospitalidfilter, hospitalnamefilter = null;
            if (doctorname != null)
            {
                doctornamefilter = l => l.Name.Contains(doctorname);
            }
            else
            {
                doctornamefilter = l => l.IsDeleted != true;
            }
            if (doctorid != 0)
            {
                doctoridfilter = l => l.ID == doctorid;
            }
            else
            {
                doctoridfilter = l => l.IsDeleted != true;
            }
            
            if (hospitalid != 0)
            {
                hospitalidfilter = l => l.HospitalID == hospitalid;
            }
            else
            {
                hospitalidfilter = l => l.IsDeleted != true;
            }
            if (hospitalname != null)
            {
                hospitalnamefilter = l => l.HospitalName.StartsWith(hospitalname);
            }
            else
            {
                hospitalnamefilter = l => l.IsDeleted != true;
            }
            if (pagesize != 0)
            {
                var skipindex = pagesize * (page - 1);
                result = string.Join(",", (from doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true).Where(doctornamefilter).Where(doctoridfilter)
                                           join dochos in dbContext.tbDoctorHospitals.Where(hospitalidfilter).Where(hospitalnamefilter)
                                           on doc.ID equals dochos.DoctorID
                                           select doc).DistinctBy(a => a.ID).Select(a => a.Specialty).OrderBy(a => a).ToList()).Split(',').Select(b => b.Trim()).Distinct().Skip(skipindex).Take(page).ToList(); //
            }
            else
            {
                result = string.Join(",", (from doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true).Where(doctornamefilter).Where(doctoridfilter)
                                           join dochos in dbContext.tbDoctorHospitals.Where(hospitalidfilter).Where(hospitalnamefilter)
                                           on doc.ID equals dochos.DoctorID
                                           select doc).DistinctBy(a => a.ID).Select(a => a.Specialty).OrderBy(a => a).ToList()).Split(',').Select(b => b.Trim()).Distinct().ToList(); //
            }
            dbContext.Dispose();
            return request.CreateResponse<List<string>>(HttpStatusCode.OK, result);
        }


        [HttpGet]
        [Route("api/Doctor/GetDocSpecialtyListFilter")]
        public HttpResponseMessage getCategoryListFilter(HttpRequestMessage request)
        {
            List<tbDoctor> doctors = dbContext.tbDoctors.Where(a => a.IsDeleted != true).ToList();
            List<tbSpecialty> sepcialties = dbContext.tbSpecialties.Where(a => a.IsDeleted != true).ToList();
            DoctorSpecialitiesViewModel dsvm = new DoctorSpecialitiesViewModel();
            dsvm.doctors = doctors;
            dsvm.specialities = sepcialties;
            return request.CreateResponse<DoctorSpecialitiesViewModel>(HttpStatusCode.OK, dsvm);
        }

        [Route("api/Doctor/getSpecialtyList")]
        [HttpGet]
        public HttpResponseMessage getSpecialtyList(HttpRequestMessage request)
        {
           // tbSpecialty specialties = new tbSpecialty();
            List<tbSpecialty> result = dbContext.tbSpecialties.Where(a => a.IsDeleted != true).ToList();
            dbContext.Dispose();
            return request.CreateResponse<List<tbSpecialty>>(HttpStatusCode.OK, result);
        }


        [HttpGet]
        [Route("api/doctor/getdoctorwithspecialty")]
        public HttpResponseMessage getitemwithcategory(HttpRequestMessage request, int doctorid)
        {
            var result = (from doc in dbContext.tbDoctors.Where(a => a.ID == doctorid && a.IsDeleted != true)
                          join spe in dbContext.tbSpecialties.Where(a => a.IsDeleted != true)
                          on doc.SpecialityID equals spe.ID
                          select new DoctorSpecialityViewModel
                          {
                              doctor = doc,
                              specialty = spe,
                          }).ToList();

            return request.CreateResponse<List<DoctorSpecialityViewModel>>(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Doctor/DoctorDelete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ID, int hospitalid = 0)
        {
            tbDoctor UpdatedDoctorEntity = new tbDoctor();
            tbDoctorHospital UpdatedDoctorHospitalEntity = new tbDoctorHospital();
            tbDoctor doctor = doctorRepo.Get().Where(a => a.ID == ID).FirstOrDefault();
            tbDoctorHospital doctorhospital = doctorhospitalRepo.Get().Where(a => a.DoctorID == doctor.ID && a.HospitalID == hospitalid).FirstOrDefault();
            doctor.IsDeleted = true;
            doctorhospital.IsDeleted = true;
            UpdatedDoctorEntity = doctorRepo.UpdatewithObj(doctor);
            UpdatedDoctorHospitalEntity = doctorhospitalRepo.UpdatewithObj(doctorhospital);
            DoctorHospitalViewModel dhvm = new DoctorHospitalViewModel();
            dhvm.doctor = UpdatedDoctorEntity;
            dhvm.hospital = UpdatedDoctorHospitalEntity;
            return request.CreateResponse<DoctorHospitalViewModel>(HttpStatusCode.OK, dhvm);
        }

        [HttpGet]
        [Route("api/Doctor/ScheduleDelete")]
        public HttpResponseMessage ScheduleDelete(HttpRequestMessage request, int ID)
        {
            tbSchedule UpdatedEntity = new tbSchedule();
           
            tbSchedule schedule = scheduleRepo.Get().Where(a => a.ID == ID).FirstOrDefault();

            schedule.IsDeleted = true;
        
            UpdatedEntity = scheduleRepo.UpdatewithObj(schedule);
       
         
            return request.CreateResponse<tbSchedule>(HttpStatusCode.OK, UpdatedEntity);
        }

        //[HttpGet]
        //[Route("api/Doctor/DoctorDelete")]
        //public HttpResponseMessage Delete(HttpRequestMessage request, int ID)
        //{
        //    tbDoctor UpdatedDoctorEntity = new tbDoctor();          
        //    tbDoctor doctor = doctorRepo.Get().Where(a => a.ID == ID).FirstOrDefault();        
        //    doctor.IsDeleted = true;        
        //    UpdatedDoctorEntity = doctorRepo.UpdatewithObj(doctor);   
        //    return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, UpdatedDoctorEntity);
        //}


        [Route("api/Doctor/UpsertDoctor")]
        [HttpPost]
        public HttpResponseMessage UpsertDoctor(HttpRequestMessage request, DoctorSpecialityViewModel dsvm)
        {

            string result = null;
            tbDoctor UpdateEntity = null;
            if(dsvm.doctor.Image != null)
            {
               result = iPhoto.uploadPhoto(dsvm.doctor.Image);
            }else
            {
                result = null;
            }
               
            if (dsvm.doctor.ID > 0)
            {
                if(dsvm.doctor.Image == null)
                {
                    tbDoctor doc = dbContext.tbDoctors.Where(a => a.IsDeleted != true && a.ID == dsvm.doctor.ID).FirstOrDefault();
                    dsvm.doctor.Photo = doc.Photo;
                } else
                {
                    dsvm.doctor.Photo = result;
                }              
                dsvm.doctor.Image = null;
                dsvm.doctor.SpecialityID = dsvm.specialty.ID;
                dsvm.doctor.Specialty = dsvm.specialty.Specialty;
                UpdateEntity = doctorRepo.UpdatewithObj(dsvm.doctor);
            }
            else
            {
                dsvm.doctor.Photo = result;
                dsvm.doctor.Image = null;
                dsvm.doctor.SpecialityID = dsvm.specialty.ID;
                dsvm.doctor.Specialty = dsvm.specialty.Specialty;
                dsvm.doctor.IsDeleted = false;              
                dsvm.doctor.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);             
                UpdateEntity = doctorRepo.AddWithGetObj(dsvm.doctor);
            }
            tbDoctorHospital dh = doctorhospitalRepo.Get().Where(a => a.DoctorID == UpdateEntity.ID).FirstOrDefault();
            if(dh != null)
            {
              
                dh.DoctorID = UpdateEntity.ID;
                dh.HospitalID = dsvm.hospitalid;
                dh.HospitalName = dsvm.hospitalname;
                dh.DoctorName = UpdateEntity.Name;
                doctorhospitalRepo.UpdatewithObj(dh);
            }else
            {
                dh = new tbDoctorHospital();
                dh.DoctorID = UpdateEntity.ID;
                dh.HospitalID = dsvm.hospitalid;
                dh.HospitalName = dsvm.hospitalname;
                dh.DoctorName = UpdateEntity.Name;
                dh.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                dh.IsDeleted = false;
                doctorhospitalRepo.AddWithGetObj(dh);
            }
        
            dsvm.doctor = UpdateEntity;
            return request.CreateResponse<DoctorSpecialityViewModel>(HttpStatusCode.OK, dsvm);

        }

        [Route("api/Doctor/GetDoctorByID")]
        [HttpGet]
        public HttpResponseMessage GetDoctorById(HttpRequestMessage request, int ID)
        {
            tbDoctor doctor = doctorRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.ID == ID).FirstOrDefault();
            tbSpecialty specialty = dbContext.tbSpecialties.Where(a => a.IsDeleted != true && a.ID == doctor.SpecialityID).FirstOrDefault();
            DoctorSpecialityViewModel dsvm = new DoctorSpecialityViewModel();
            dsvm.doctor = doctor;
            dsvm.specialty = specialty;

            return request.CreateResponse<DoctorSpecialityViewModel>(HttpStatusCode.OK, dsvm);
        }

        [Route("api/Doctor/getScheduleByID")]
        [HttpGet]
        public HttpResponseMessage getScheduleByID(HttpRequestMessage request, int ID)
        {
            tbSchedule cate = new tbSchedule();
            var result = dbContext.tbSchedules.Where(a => a.IsDeleted != true && a.ID == ID).FirstOrDefault();
            return request.CreateResponse<tbSchedule>(HttpStatusCode.OK, result);
        }


        
        [Route("api/Doctor/scheduleListbyDoctorID")]
        [HttpGet]
        public HttpResponseMessage scheduleListbyDoctorID(HttpRequestMessage request, int doctorid = 0)
        {
            var result = dbContext.tbSchedules.Where(a => a.IsDeleted != true && a.DoctorID == doctorid).ToList();
            return request.CreateResponse<List<tbSchedule>>(HttpStatusCode.OK, result);
        }


        [Route("api/Doctor/getgenerateschedulelist")]
        [HttpGet]
        public HttpResponseMessage getgenerateschedulelist(HttpRequestMessage request, int doctorid = 0)
        {
            var result = dbContext.tbSchedules.Where(a => a.IsDeleted != true && a.DoctorID == doctorid).ToList();
            return request.CreateResponse<List<tbSchedule>>(HttpStatusCode.OK, result);
        }

        [Route("api/Doctor/getdoctorlistbyhospital")]
        [HttpGet]
        public HttpResponseMessage getdoctorlistbyhospital(HttpRequestMessage request, int hospitalID = 0)
        {
            var result = dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == hospitalID).OrderBy(a=>a.DoctorName).ToList();
            return request.CreateResponse<List<tbDoctorHospital>>(HttpStatusCode.OK, result);
        }
       

        [Route("api/Doctor/UpsertSchedule")]
        [HttpPost]
        public HttpResponseMessage UpsertSchedule(HttpRequestMessage request, tbScheduleData schedule)
        {
            string date = schedule.AppointmentDatetime.Value.ToShortDateString();
            string fromtime = schedule.Fromtime.Value.ToShortTimeString();
            string totime = schedule.Totime.Value.ToShortTimeString();
            fromtime = date + " " + fromtime;
            totime = date + " " + totime;
            DateTime fromtimedate = DateTime.Parse(fromtime);
            DateTime totimedate = DateTime.Parse(totime);

            tbScheduleData UpdatedEntity = null;
            if (schedule.ID > 0)
            {
                schedule.Fromtime = fromtimedate;
                schedule.Totime = totimedate;
                schedule.AppointmentDatetime = fromtimedate;
                UpdatedEntity = scheduledataRepo.UpdatewithObj(schedule);
            }
            else
            {
               
                schedule.IsDeleted = false;
                schedule.Fromtime = fromtimedate;
                schedule.Totime = totimedate;
                schedule.AppointmentDatetime = fromtimedate;
                UpdatedEntity = scheduledataRepo.AddWithGetObj(schedule);
            }
            return request.CreateResponse<tbScheduleData>(HttpStatusCode.OK, UpdatedEntity);
        }


        [Route("api/Doctor/UpsertSchedule2")]
        [HttpPost]
        public HttpResponseMessage UpsertSchedule(HttpRequestMessage request, tbSchedule schedule)
        {

            tbSchedule UpdatedEntity = null;
            if (schedule.ID > 0)
            {
                UpdatedEntity = scheduleRepo.UpdatewithObj(schedule);
            }
            else
            {

                schedule.IsDeleted = false;
                UpdatedEntity = scheduleRepo.AddWithGetObj(schedule);
            }
            return request.CreateResponse<tbSchedule>(HttpStatusCode.OK, UpdatedEntity);
        }


        [Route("api/Doctor/UpSertGenerateSchedule")]
        [HttpGet]
        public HttpResponseMessage UpSertGenerateSchedule(HttpRequestMessage request, DateTime? fromdatepicker = null, DateTime? todatepicker = null, int doctorid = 0, int hospitalid = 0, string hospitalname = null)
        {

            List<DateTime> days_list = new List<DateTime>();
            DateTime temp_start = fromdatepicker.Value.Date;
            DateTime temp_end = todatepicker.Value.Date;       
            //temp_start = new DateTime(fromdatepicker.Value.Year, fromdatepicker.Value.Month, fromdatepicker.Value.Day);
            //temp_end = new DateTime(todatepicker.Value.Year, todatepicker.Value.Month, todatepicker.Value.Day);
            for (DateTime date = temp_start; date <= temp_end; date = date.AddDays(1))
            {
                days_list.Add(date);
            }

            List<tbScheduleData> schdatalist = new List<tbScheduleData>();

            foreach(var item in days_list)
            {
                var day = item.DayOfWeek.ToString();
                List<tbSchedule> schedules = dbContext.tbSchedules.Where(a => a.IsDeleted != true && a.DoctorID == doctorid).Where(iQueryService.daysQuery(day)).ToList();
                foreach(var sch in schedules)
                {
                    //added skip existed scheduledata
                    var appointmentDatetime = HelperExtension.getCombinedDateTime(item, sch.Fromtime);
                    tbScheduleData existedData = dbContext.tbScheduleDatas.Where(a => a.DoctorID == doctorid && a.HospitalID == hospitalid
                                                  && a.AppointmentDatetime == appointmentDatetime).FirstOrDefault();
                    if(existedData == null)
                    {
                        tbScheduleData sd = new tbScheduleData();
                        sd.AppointmentDatetime = appointmentDatetime;  // HelperExtension.getCombinedDateTime(item, sch.Fromtime);
                        sd.Day = day;
                        sd.HospitalID = hospitalid;
                        sd.HospitalName = hospitalname;
                        sd.DoctorID = doctorid;
                        sd.DoctorName = dbContext.tbDoctors.Where(a => a.ID == doctorid).Select(a => a.Name).FirstOrDefault();
                        sd.ScheduleID = sch.ID;
                        sd.Fromtime = sch.Fromtime;
                        sd.Totime = sch.Totime;
                        sd.MaxPatientCount = sch.PatientLimit;
                        sd.DefaultPatientCount = sch.PatientLimit;
                        sd.AppointmentTime = sch.Fromtime.Value.ToShortDateString();
                        sd = scheduledataRepo.AddWithGetObj(sd);
                    }
                   
                }
            }
            return request.CreateResponse<tbScheduleData>(HttpStatusCode.OK, null);
        }

        static List<DateTime> GetDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();

            while ((startDate = startDate.AddDays(1)) < endDate)
            {
                dates.Add(startDate);
            }

            return dates;
        }


        [HttpGet]
        [Route("api/Doctor/DoctorList")]
        public HttpResponseMessage DoctorList(HttpRequestMessage request)
        {
            List<tbDoctor> doctors = dbContext.tbDoctors.Where(a => a.IsDeleted != true).ToList();
            return request.CreateResponse<List<tbDoctor>>(HttpStatusCode.OK, doctors);
        }


    }

}

