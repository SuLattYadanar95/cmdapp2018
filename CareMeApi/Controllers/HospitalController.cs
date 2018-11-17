using CareMeApi.IService;
using CareMeApi.Repository;
using Data.Models;
using Data.ViewModels;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoreLinq;

namespace CareMeApi.Controllers
{
    public class HospitalController : ApiController
    {
        CaremeDBContext dbContext;
        private HospitalRepository hospitalRepo = null;
        private DoctorHospitalRepository docHosRepo = null;
        private readonly IPhoto iPhoto;
        public HospitalController(IPhoto iPhoto)
        {
            this.iPhoto = iPhoto;
            dbContext = new CaremeDBContext();
            hospitalRepo = new HospitalRepository(dbContext);
            docHosRepo = new DoctorHospitalRepository(dbContext);
        }

        //scheduledHospitalList
        //int doctorID, int hospitalID = 0

        [HttpGet, Route("api/Hospital/scheduledHospitalList")]
        public HttpResponseMessage scheduledHospitalList(HttpRequestMessage request, int doctorID, int hospitalID = 0, DateTime? AppointmentDate = null)
        {
            Expression<Func<tbHospital, bool>> hospitalfilter;
            Expression<Func<tbScheduleData, bool>> hospitalSDfilter;
            Expression<Func<tbScheduleData, bool>> appointmentDateFilter;
            if (hospitalID > 0)
            {
                hospitalfilter = h => h.ID == hospitalID;
                hospitalSDfilter = h => h.HospitalID == hospitalID;
            }
            else
            {
                hospitalfilter = h => h.IsDeleted != true;
                hospitalSDfilter = h => h.IsDeleted != true;
            }
            if (AppointmentDate != null)
            {
                DateTime stdate = AppointmentDate.Value.Date;
                DateTime eddate = AppointmentDate.Value.Date.AddDays(1);
                appointmentDateFilter = a => a.AppointmentDatetime >= stdate && a.AppointmentDatetime <= eddate;

            }
            else
            {
                appointmentDateFilter = a => a.IsDeleted != true;
            }
            var now = MyExtension.getLocalTime(DateTime.UtcNow);

            List<HospitalSchedules> result = (from h in dbContext.tbHospitals.Where(a => a.IsDeleted != true).Where(hospitalfilter)
                                              join hsd in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.DoctorID == doctorID && a.AppointmentDatetime >= now)
                                              .Where(hospitalSDfilter) on h.ID equals hsd.HospitalID //.Where(appointmentDateFilter)
                                              select new HospitalSchedules
                                              {
                                                  tbHospital = h,
                                                  scheduleDataList = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true
                                                                    && a.DoctorID == doctorID && a.HospitalID == h.ID && a.AppointmentDatetime > now)
                                                                    .Where(hospitalSDfilter).OrderBy(a=>a.AppointmentDatetime).ToList(),  //.Where(appointmentDateFilter)
                                              }).DistinctBy(h => h.tbHospital.ID).ToList();

            return request.CreateResponse<List<HospitalSchedules>>(HttpStatusCode.OK, result);

        }


        [Route("api/Hospital/list")]
        [HttpGet]
        public HttpResponseMessage List(HttpRequestMessage request,string hospitalname = null, int pagesize = 10, int page = 1)
        {
            Expression<Func<tbHospital, bool>> hospitalnamefilter;
           
            if (hospitalname != null)
            {
                hospitalnamefilter = l => l.Name.Contains(hospitalname);
            }
            else
            {
                hospitalnamefilter = l => l.IsDeleted != true;
            }

            IQueryable<tbHospital> result = dbContext.tbHospitals.Where(a => a.IsDeleted != true).Where(hospitalnamefilter);
            var skipindex = pagesize * (page - 1);
            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.Accesstime)
                         .Skip(skipindex).Take(pagesize);
            PagedListServer<tbHospital> model = new PagedListServer<tbHospital>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<tbHospital>>(HttpStatusCode.OK, model);
        }
        [Route("api/Hospital/get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string township = null, int pagesize = 10, int page = 1)
        {
            List<tbHospital> results = null;
            Expression<Func<tbHospital, bool>> townshipfilter;
            if (township != null)
            {
                townshipfilter = l => l.Township.StartsWith(township);
            }
            else
            {
                townshipfilter = l => l.IsDeleted != true;
            }
            if (pagesize != 0)
            {
                var skipindex = pagesize * (page - 1);
                results = dbContext.tbHospitals.Where(a => a.IsDeleted != true).Where(townshipfilter).OrderBy(a => a.Name).Skip(skipindex).Take(page).ToList();
            }
            else
            {
                results = dbContext.tbHospitals.Where(a => a.IsDeleted != true).Where(townshipfilter).OrderBy(a => a.Name).ToList();
            }
            return request.CreateResponse<List<tbHospital>>(HttpStatusCode.OK, results);
        }


        [HttpGet]
        [Route("api/hospital/hospitaldelete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int ID)
        {
            tbHospital UpdatedEntity = new tbHospital();
            tbHospital hospital = hospitalRepo.Get().Where(a => a.ID == ID).FirstOrDefault();
            hospital.IsDeleted = true;

            UpdatedEntity = hospitalRepo.UpdatewithObj(hospital);

            List<tbDoctorHospital> doctorhospital = dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == ID).ToList();
            foreach(var item in doctorhospital)
            {
                item.IsDeleted = true;
                docHosRepo.Update(item);
            }
          
            return request.CreateResponse<tbHospital>(HttpStatusCode.OK, UpdatedEntity);
        }

      

        [Route("api/hospital/UpsertHospital")]
        [HttpPost]
        public HttpResponseMessage UpsertDoctor(HttpRequestMessage request, tbHospital hospital)
        {

            string result = null;
            tbHospital UpdateEntity = null;
            if (hospital.Image != null)
            {
                result = iPhoto.uploadPhoto(hospital.Image);
            }
            else
            {
                result = null;
            }

            if (hospital.ID > 0)
            {
                if (hospital.Image == null)
                {
                    tbHospital hos = dbContext.tbHospitals.Where(a => a.IsDeleted != true && a.ID == hospital.ID).FirstOrDefault();
                    hospital.Photo = hos.Photo;
                }
                else
                {
                    hospital.Photo = result;
                }
                hospital.Image = null;
                UpdateEntity = hospitalRepo.UpdatewithObj(hospital);
            }
            else
            {
                hospital.Photo = result;
                hospital.Image = null;
                hospital.IsDeleted = false;
                hospital.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                UpdateEntity = hospitalRepo.AddWithGetObj(hospital);
            }
         
            return request.CreateResponse<tbHospital>(HttpStatusCode.OK, UpdateEntity);

        }

        [Route("api/hospital/GetHospitalById")]
        [HttpGet]
        public HttpResponseMessage GetDoctorById(HttpRequestMessage request, int ID)
        {
            tbHospital hospital = hospitalRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.ID == ID).FirstOrDefault();
            

            return request.CreateResponse<tbHospital>(HttpStatusCode.OK, hospital);
        }


        [Route("api/hospital/GetState")]
        [HttpGet]
        public HttpResponseMessage GetState(HttpRequestMessage request)
        {
            List<tbLocation> location = dbContext.tbLocations.Where(a => a.IsDeleted != true).DistinctBy(a => a.StateDivision).ToList();
            return request.CreateResponse<List<tbLocation>>(HttpStatusCode.OK, location);
        }


        [Route("api/hospital/GetTownShip")]
        [HttpGet]
        public HttpResponseMessage GetTownShip(HttpRequestMessage request, string state = null)
        {
            IEnumerable<string> township = dbContext.tbLocations.Where(a => a.IsDeleted != true && a.StateDivision == state).Select(a => a.Township).ToList();
            return request.CreateResponse<IEnumerable<string>>(HttpStatusCode.OK, township);
        }


    }
}
