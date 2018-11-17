using CareMeMobileApi.IService;
using CareMeMobileApi.Repository;
using CareMeMobileApi.Services;
using Data.Models;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeMobileApi.Controllers
{
    public class DoctorController : ApiController
    {
        CaremeDBContext dbContext;

        private ScheduleRepository scheduleRepo = null;
        private HospitalRepository hospitalRepo = null;
        private DoctorRepository doctorRepo = null;
        private AppointmentRepository appointmentRepo = null;
        private SpecialityRepository specialityRepo = null;

        public DoctorController()
        {
            dbContext = new CaremeDBContext();
            scheduleRepo = new ScheduleRepository(dbContext);
            hospitalRepo = new HospitalRepository(dbContext);
            doctorRepo = new DoctorRepository(dbContext);
            appointmentRepo = new AppointmentRepository(dbContext);
            specialityRepo = new SpecialityRepository(dbContext);
        }


       
        [HttpPost]
        [Route("api/doctor/login")]
        public HttpResponseMessage Login(HttpRequestMessage request, tbDoctor doc)
        {

            tbDoctor result = doctorRepo.GetWithoutTracking().Where(a => a.Phone == doc.Phone && a.Pin == doc.Pin).FirstOrDefault();
            return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/doctor/logout")]
        public HttpResponseMessage logout(HttpRequestMessage request, int docid)
        {

            tbDoctor result = doctorRepo.GetWithoutTracking().Where(a => a.ID == docid && a.IsDeleted != true).FirstOrDefault();
            result.UserToken = null;
            result = doctorRepo.UpdatewithObj(result);
            return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/doctor/mapToken")]
        public HttpResponseMessage mapToken(HttpRequestMessage request, tbDoctor doc)
        {
            tbDoctor result = doctorRepo.GetWithoutTracking().Where(a => a.ID == doc.ID && a.IsDeleted != true).FirstOrDefault();
            result.UserToken = doc.UserToken;
            result = doctorRepo.UpdatewithObj(result);
            return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/doctor/addSpeciality")]
        public HttpResponseMessage addSpeciality(HttpRequestMessage request, int doctorid = 0, int specialityid = 0)
        {
            tbDoctor UpdatedEntity = new tbDoctor();
            tbSpecialty specialty = specialityRepo.Get().Where(s => s.ID == specialityid).FirstOrDefault();
            tbDoctor doctor = doctorRepo.Get().Where(d => d.ID == doctorid).FirstOrDefault();
            doctor.SpecialityID = specialty.ID;
            doctor.Specialty = specialty.Specialty;
            UpdatedEntity = doctorRepo.UpdatewithObj(doctor);
            return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, UpdatedEntity);
        }


        [HttpGet]
        [Route("api/doctor/resetpin")]
        public HttpResponseMessage ResetPin(HttpRequestMessage request, int doctorid, string pin)
        {
            tbDoctor UpdatedEntity = new tbDoctor();
            tbDoctor result = doctorRepo.Get().Where(a => a.ID == doctorid).FirstOrDefault();
            result.Pin = pin;
            UpdatedEntity = doctorRepo.UpdatewithObj(result);
            return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, UpdatedEntity);
        }


        [HttpPost, Route("api/doctor/UpSertDOctor")]
        public HttpResponseMessage UpSertDOctor(HttpRequestMessage request, tbDoctor doc)
        {
            tbDoctor UpdatedEntity = new tbDoctor();
            IPhoto iPhoto = new AzurePhotoUpload();

            if (doc.ID > 0)
            {
                tbSpecialty specialty = specialityRepo.GetWithoutTracking().Where(s => s.ID == doc.SpecialityID).FirstOrDefault();
                if (specialty != null)
                {
                    doc.SpecialityID = doc.SpecialityID;
                    doc.Specialty = specialty.Specialty;
                }

                tbDoctor doctor = doctorRepo.GetWithoutTracking().Where(d => d.ID == doc.ID).FirstOrDefault();
                if (doc.Image == null)
                {
                    doc.Photo = doctor.Photo;
                }
                else
                {
                    doc.Photo = iPhoto.uploadPhoto(doc.Image);
                    doc.Image = null;
                }
                UpdatedEntity = doctorRepo.UpdatewithObj(doc);
            }
            else
            {
                var result = (from t in dbContext.tbDoctors
                              where t.Phone == doc.Phone
                              select t).Any();
                if (result == false)
                {
                    tbSpecialty specialty = specialityRepo.Get().Where(s => s.ID == doc.SpecialityID).FirstOrDefault();
                    if (specialty != null)
                    {
                        doc.SpecialityID = doc.SpecialityID;
                        doc.Specialty = specialty.Specialty;
                    }
                    doc.IsDeleted = false;
                    doc.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                    if (doc.Image != null)
                    {
                        doc.Photo = iPhoto.uploadPhoto(doc.Image);
                        doc.Image = null;
                    }
                    UpdatedEntity = doctorRepo.AddWithGetObj(doc);
                }
                else
                {
                    UpdatedEntity.SystemStatus = "E002"; //
                }
            }
            return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, UpdatedEntity);
        }

        [Route("api/doctor/doctorDetail")]
        [HttpGet]
        public HttpResponseMessage doctorDetail(HttpRequestMessage request, int doctorID)
        {

            tbDoctor result = doctorRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.ID == doctorID).FirstOrDefault();

            return request.CreateResponse<tbDoctor>(HttpStatusCode.OK, result);
        }

        [Route("api/doctor/doctorlist")]
        [HttpGet]
        public HttpResponseMessage doctorlist(HttpRequestMessage request)
        {

            List<tbDoctor> result = doctorRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).ToList();

            return request.CreateResponse<List<tbDoctor>>(HttpStatusCode.OK, result);
        }



    }
}
