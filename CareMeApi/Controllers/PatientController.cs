using CareMeApi.IService;
using CareMeApi.Repository;
using Core.Extensions;
using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeApi.Controllers
{
    public class PatientController : ApiController
    {
        CaremeDBContext dbContext;
        private PatientRepository patientRepo = null;
        private AppointmentRepository appRepo = null;
        private DoctorRepository docRepo = null;

        private readonly IBookingService iBookingService;

        public PatientController(IBookingService iBookingService)
        {
            this.iBookingService = iBookingService;

            dbContext = new CaremeDBContext();
            patientRepo = new PatientRepository(dbContext);
            appRepo = new AppointmentRepository(dbContext);
            docRepo = new DoctorRepository(dbContext);
        }


        [Route("api/Patient/UpsertPatient")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PatientAppointmentViewModel patient)
        {
            PatientAppointmentViewModel pavm = new PatientAppointmentViewModel();

            tbPatient UpdatedEntity = null;
            tbAppointment UpdatedAppointment = null;

            if(iBookingService.checkBookingAvailable(patient.appointment.ScheduleDataID ?? 0) == true)
            {
                if (patient.patient.ID > 0)
                {
                    UpdatedEntity = patientRepo.UpdatewithObj(patient.patient);
                }
                else
                {
                    if(patient.patient.MsgrID != null)
                    {
                        tbPatient oldPatient = patientRepo.Get().Where(a => a.MsgrID == patient.patient.MsgrID && a.IsDeleted != true).FirstOrDefault();
                        if(oldPatient != null)
                        {
                            oldPatient.MsgrName = patient.patient.MsgrName;
                            oldPatient.Name = patient.patient.Name;
                            oldPatient.Phone = patient.patient.Phone;
                            oldPatient.Problem = patient.patient.Problem;
                            oldPatient.Gender = patient.patient.Gender;
                            oldPatient.Age = patient.patient.Age;
                            oldPatient.Address = patient.patient.Address;
                            UpdatedEntity = patientRepo.UpdatewithObj(oldPatient);
                        }
                        else
                        {
                            // UpdatedEntity = new tbPatient();
                            patient.patient.IsDeleted = false;
                            patient.patient.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                            UpdatedEntity = patientRepo.AddWithGetObj(patient.patient);
                        }
                    }
                    else
                    {
                        // UpdatedEntity = new tbPatient();
                        patient.patient.IsDeleted = false;
                        patient.patient.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                        UpdatedEntity = patientRepo.AddWithGetObj(patient.patient);
                    }
                   
                }

                tbAppointment appointment = appRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.PatientId == UpdatedEntity.ID).FirstOrDefault();
                if (appointment != null)
                {

                    appointment.PatientId = UpdatedEntity.ID;
                    appointment.PatientName = UpdatedEntity.Name;
                    appointment.PatientAge = UpdatedEntity.Age;
                    appointment.DoctorName = dbContext.tbDoctors.Where(a => a.IsDeleted != true && a.ID == patient.appointment.DoctorId).Select(a => a.Name).FirstOrDefault();
                    appointment = appRepo.UpdatewithObj(appointment);
                }
                else
                {
                    var scheduledata = dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.ID == patient.appointment.ScheduleDataID).FirstOrDefault();

                    int counter = dbContext.tbAppointments.Where(a => a.ScheduleDataID == scheduledata.ID).Count();
                    int? position = dbContext.tbAppointments.Where(a => a.ScheduleDataID == scheduledata.ID).Max(a => a.Position);
                    // patient.appointment = new tbAppointment();
                    patient.appointment.PatientId = UpdatedEntity.ID;
                    patient.appointment.PatientName = UpdatedEntity.Name;
                    patient.appointment.PatientAge = UpdatedEntity.Age;
                    patient.appointment.Status = "BOOKED";
                    patient.appointment.Counter = counter + 1;
                    if (position == null)
                    {
                        patient.appointment.Position = counter + 1;
                    }
                    else
                    {
                        patient.appointment.Position = position + 1;
                    }
                    //patient.appointment.HospitalId = 
                    //patient.appointment.HospitalName = 
                    //    patient.appointment.Day = patient.appointment.AppointmentDateTime.Value.DayOfWeek.ToString();
                    patient.appointment.IsDeleted = false;
                    patient.appointment.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                    patient.appointment.HospitalName = dbContext.tbHospitals.Where(a => a.IsDeleted != true && a.ID == patient.appointment.HospitalId).Select(a => a.Name).FirstOrDefault();
                    //   patient.appointment.DoctorName = dbContext.tbDoctors.Where(a => a.IsDeleted != true && a.ID == patient.appointment.DoctorId).Select(a => a.Name).FirstOrDefault();
                    UpdatedAppointment = appRepo.AddWithGetObj(patient.appointment);

                    var doc = docRepo.Get().Where(a => a.ID == patient.appointment.DoctorId && a.IsDeleted != true).FirstOrDefault();

                    FCMViewModel fcm = new FCMViewModel();
                    fcm.to = doc.UserToken;

                    fcmdata fcmdata = new fcmdata();
                    fcmdata.body = "1 patient is waiting";
                    fcmdata.doctorId = doc.ID;
                    fcmdata.title = "Booking notification";
                    fcmdata.type = "1001";
                    fcm.data = fcmdata;
                    FCMRequestHelper.sendTokenMessage(fcm);
                }
            }

            pavm.patient = UpdatedEntity;
            pavm.appointment = UpdatedAppointment;

            return request.CreateResponse<PatientAppointmentViewModel>(HttpStatusCode.OK, pavm);
        }
    }
}
