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
    public class AppointmentController : ApiController
    {
        CaremeDBContext dbContext;
        private AppointmentRepository appointmentRepo = null;

        public AppointmentController()
        {
            dbContext = new CaremeDBContext();
            appointmentRepo = new AppointmentRepository(dbContext);
        }
        [Route("api/appointment/bookinglist")]
        [HttpGet]
        public HttpResponseMessage bookinglist(HttpRequestMessage request, string name = null, string status = null, int pagesize = 10, int page = 1, DateTime? datetime = null, DateTime? time = null, int hospitalid = 2, int doctorid = 0)
        {
            DateTime? newdate = null;
            Expression<Func<tbAppointment, bool>> namefilter, statusfilter, hospitalidfilter, doctoridfilter = null;
            if (name != null)
            {
                namefilter = l => l.PatientName.Contains(name);
            }
            else
            {
                namefilter = l => l.IsDeleted != true;
            }
            if (status != null)
            {
                statusfilter = l => l.Status == status;
            }
            else
            {
                statusfilter = l => l.IsDeleted != true;
            }
            if (hospitalid > 0)
            {
                hospitalidfilter = l => l.HospitalId == hospitalid;
            }
            else
            {
                hospitalidfilter = l => l.IsDeleted != true;
            }
            if (doctorid > 0)
            {
                doctoridfilter = l => l.DoctorId == doctorid;
            }
            else
            {
                doctoridfilter = l => l.IsDeleted != true;
            }
            IQueryable<tbAppointment> result = null;

            if (time != null)
            {
                result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != "CHECKOUT" && a.Status != "CANCEL" /*&& a.AppointmentDateTime.Value.Day == time.Value.Day*/ && a.AppointmentDateTime == time).Where(hospitalidfilter).Where(doctoridfilter);
            }

            else
            {
                newdate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.AppointmentDateTime >= newdate && a.Status != "CHECKOUT" && a.Status != "CANCEL").Where(namefilter).Where(statusfilter).Where(hospitalidfilter);
            }          
            PagedListServer<tbAppointment> model = new PagedListServer<tbAppointment>(result.OrderBy(a => a.IsEmergency).OrderBy(a => a.Accesstime).OrderBy(a => a.Position), pagesize, page);
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<tbAppointment>>(HttpStatusCode.OK, model);
        }
        //[Route("api/appointment/bookinglist")]
        //[HttpGet]
        //public HttpResponseMessage bookinglist(HttpRequestMessage request, string name = null, string status = null, int pagesize = 10, int page = 1, DateTime? datetime = null, DateTime? time = null, int hospitalid = 0, int doctorid = 0)
        //{
        //    DateTime? newdate = null;
        //    Expression<Func<tbAppointment, bool>> namefilter, statusfilter, hospitalidfilter, doctoridfilter = null;
        //    if (name != null)
        //    {
        //        namefilter = l => l.PatientName.Contains(name);
        //    }
        //    else
        //    {
        //        namefilter = l => l.IsDeleted != true;
        //    }
        //    if (status != null)
        //    {
        //        statusfilter = l => l.Status == status;
        //    }
        //    else
        //    {
        //        statusfilter = l => l.IsDeleted != true;
        //    }
        //    if (hospitalid > 0)
        //    {
        //        hospitalidfilter = l => l.HospitalId == hospitalid;
        //    }
        //    else
        //    {
        //        hospitalidfilter = l => l.IsDeleted != true;
        //    }
        //    if (doctorid > 0)
        //    {
        //        doctoridfilter = l => l.DoctorId == doctorid;
        //    }
        //    else
        //    {
        //        doctoridfilter = l => l.IsDeleted != true;
        //    }
        //    IQueryable<tbAppointment> result = null;
        //    //if (datetime == null)
        //    //{
        //    //    newdate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
        //    //    //newdate = Convert.ToDateTime(datetime.Value.ToShortDateString() + " " + datetime.Value.ToShortTimeString());
        //    //    result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.AppointmentDateTime >= newdate && a.Status != "CHECKOUT" && a.Status != "CANCEL").Where(namefilter).Where(statusfilter).Where(hospitalidfilter);
        //    //}
        //    //else
        //    //{
        //    //    result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != "CHECKOUT" && a.Status != "CANCEL" && a.AppointmentDateTime.Value.Day == datetime.Value.Day).Where(namefilter).Where(statusfilter).Where(hospitalidfilter).Where(doctoridfilter);
        //    //}
        //    if (doctorid > 0)
        //    {
        //        result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != "CHECKOUT" && a.Status != "CANCEL" && a.AppointmentDateTime.Value.Day == datetime.Value.Day).Where(namefilter).Where(statusfilter).Where(hospitalidfilter).Where(doctoridfilter);
        //    }
        //    if (time != null)
        //    {
        //        result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != "CHECKOUT" && a.Status != "CANCEL" /*&& a.AppointmentDateTime.Value.Day == time.Value.Day*/ && a.AppointmentDateTime == time).Where(hospitalidfilter).Where(doctoridfilter);
        //    }
        //    else
        //    {
        //        result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != "CHECKOUT" && a.Status != "CANCEL" && a.AppointmentDateTime.Value.Day == datetime.Value.Day).Where(namefilter).Where(statusfilter).Where(hospitalidfilter).Where(doctoridfilter);
        //    }
        //    if (datetime == null)
        //    {
        //        newdate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
        //        //newdate = Convert.ToDateTime(datetime.Value.ToShortDateString() + " " + datetime.Value.ToShortTimeString());
        //        result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.AppointmentDateTime >= newdate && a.Status != "CHECKOUT" && a.Status != "CANCEL").Where(namefilter).Where(statusfilter).Where(hospitalidfilter);
        //    }

        //    var totalCount = result.Count();
        //    var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
        //    var dataList = result.OrderBy(a => a.IsEmergency).OrderBy(a => a.Accesstime).OrderBy(a => a.Position)
        //                 .Skip(pagesize * (page - 1)).Take(pagesize);
        //    PagedListServer<tbAppointment> model = new PagedListServer<tbAppointment>();
        //    model.Results = dataList.ToList();
        //    model.TotalCount = totalCount;
        //    model.TotalPages = totalpages;
        //    dbContext.Dispose();
        //    return request.CreateResponse<PagedListServer<tbAppointment>>(HttpStatusCode.OK, model);
        //}


        [HttpGet]
        [Route("api/appointment/statuschange")]
        public HttpResponseMessage statuschange(HttpRequestMessage request, int id, string statuschange = null)
        {
            // tbAppointment appointment = new tbAppointment();
            tbAppointment UpdatedAppointment = null;

            tbAppointment appointment = appointmentRepo.Get().Where(a => a.ID == id).FirstOrDefault();
            appointment.Status = statuschange;
            UpdatedAppointment = appointmentRepo.UpdatewithObj(appointment);
            List<tbAppointment> appointments = appointmentRepo.Get().Where(a => a.Status == "ACCEPTED" && a.Counter < appointment.Counter).ToList();
            string time1 = appointment.AppointmentDateTime.Value.ToLongTimeString();
            //tbAppointment mama = appointmentRepo.Get().Where(a => a.ID == 4).FirstOrDefault();
            //string time2 = mama.AppointmentDateTime.Value.ToLongTimeString();
            //TimeSpan duration = DateTime.Parse(time1).Subtract(DateTime.Parse(time2));
            foreach (var acceptlist in appointments)
            {
                string time2 = acceptlist.AppointmentDateTime.Value.ToLongTimeString();
                TimeSpan duration = DateTime.Parse(time1).Subtract(DateTime.Parse(time2));
                TimeSpan comTime = new TimeSpan(01, 00, 00);
                if (duration >= comTime)
                {
                    acceptlist.Status = "CANCEL";
                    appointmentRepo.UpdatewithObj(acceptlist);
                }
                //  string t1 = "01:00:00";
                //   TimeSpan t2 = DateTime.Parse(t1);
                // if (duration.ToString().Equals(t1))
                //{
                //     acceptlist.Status = "CANCEL";
                //      appointmentRepo.UpdatewithObj(acceptlist);
                //  }
            }
            return request.CreateResponse<tbAppointment>(HttpStatusCode.OK, UpdatedAppointment);
        }

        [HttpGet]
        [Route("api/appointment/newstatuschange")]
        public HttpResponseMessage newstatuschange(HttpRequestMessage request, int id, string statuschange = null, int pagesize = 10, int page = 1)
        {
            // tbAppointment appointment = new tbAppointment();
            tbAppointment UpdatedAppointment = null;

            tbAppointment appointment = appointmentRepo.Get().Where(a => a.ID == id).FirstOrDefault();
            if (statuschange == "BOOKED")
            {
                appointment.Status = null;
                appointment.IsWaiting = true;
                appointment.IsApproved = true;
            }
            else if (statuschange == "WAITING")
            {
                appointment.Status = null;
                //     appointment.IsWaiting = false; //updated
                appointment.IsCheckIn = true;
            }
            else if (statuschange == "CHECKIN")
            {
                //   appointment.IsWaiting = false; //updated
                //   appointment.IsCheckIn = false;
                appointment.Status = "CHECKOUT";
            }
            else if (statuschange == "CANCEL")
            {
                appointment.Status = "CANCEL";
                appointment.IsDelByAdmin = true;
            }
            else if (statuschange == "SKIP")
            {
                if (appointment.SkipCount == null)
                {
                    appointment.SkipCount = 1;
                    appointment.Position += 5;
                    appointment.CreatedTime = MyExtension.getLocalTime(DateTime.UtcNow);
                }
                else
                {
                    appointment.Status = "CANCEL";
                    appointment.IsDelByAdmin = true;
                }
            }

            UpdatedAppointment = appointmentRepo.UpdatewithObj(appointment);
            List<tbAppointment> result = appointmentRepo.Get().Where(a => a.Status != "CHECKOUT" && a.Status != "CANCEL").ToList();

            //return request.CreateResponse<List<tbAppointment>>(HttpStatusCode.OK, appointments);
            var totalCount = result.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var dataList = result.OrderBy(a => a.IsEmergency).OrderBy(a => a.Accesstime).OrderBy(a => a.Position)
                         .Skip(pagesize * (page - 1)).Take(pagesize);
            PagedListServer<tbAppointment> model = new PagedListServer<tbAppointment>();
            model.Results = dataList.ToList();
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            dbContext.Dispose();
            return request.CreateResponse<PagedListServer<tbAppointment>>(HttpStatusCode.OK, model);

        }


        //[HttpGet]
        //[Route("api/appointment/newstatuschange")]
        //public HttpResponseMessage newstatuschange(HttpRequestMessage request, int id, string statuschange = null, int pagesize = 10, int page = 1)
        //{
        //    // tbAppointment appointment = new tbAppointment();
        //    tbAppointment UpdatedAppointment = null;

        //    tbAppointment appointment = appointmentRepo.Get().Where(a => a.ID == id).FirstOrDefault();
        //    if (statuschange == "BOOKED")
        //    {
        //        appointment.Status = null;
        //        appointment.IsWaiting = true;
        //        appointment.IsApproved = true;
        //    }
        //    else if (statuschange == "WAITING")
        //    {
        //        appointment.Status = null;
        //       // appointment.IsWaiting = false; //updated
        //        appointment.IsCheckIn = true;
        //    }
        //    else if (statuschange == "CHECKIN")
        //    {
        //        //appointment.IsWaiting = false; //updated
        //        //appointment.IsCheckIn = false;
        //        appointment.Status = "CHECKOUT";
        //    }
        //    else if (statuschange == "CANCEL")
        //    {
        //        appointment.Status = "CANCEL";
        //        appointment.IsDelByAdmin = true;
        //    }
        //    else if (statuschange == "SKIP")
        //    {
        //        if (appointment.SkipCount == null)
        //        {
        //            appointment.SkipCount = 1;
        //            appointment.Position += 5;
        //            appointment.CreatedTime = MyExtension.getLocalTime(DateTime.UtcNow);
        //        }
        //        else
        //        {
        //            appointment.Status = "CANCEL";
        //            appointment.IsDelByAdmin = true;
        //        }
        //    }

        //    UpdatedAppointment = appointmentRepo.UpdatewithObj(appointment);
        //    List<tbAppointment> result = appointmentRepo.Get().Where(a => a.Status != "CHECKOUT" && a.Status != "CANCEL").ToList();

        //    //return request.CreateResponse<List<tbAppointment>>(HttpStatusCode.OK, appointments);
        //    var totalCount = result.Count();
        //    var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
        //    var dataList = result.OrderBy(a => a.IsEmergency).OrderBy(a => a.Accesstime).OrderBy(a => a.Position)
        //                 .Skip(pagesize * (page - 1)).Take(pagesize);
        //    PagedListServer<tbAppointment> model = new PagedListServer<tbAppointment>();
        //    model.Results = dataList.ToList();
        //    model.TotalCount = totalCount;
        //    model.TotalPages = totalpages;
        //    dbContext.Dispose();
        //    return request.CreateResponse<PagedListServer<tbAppointment>>(HttpStatusCode.OK, model);

        //}

        [HttpGet]
        [Route("api/appointment/timelist")]
        public HttpResponseMessage timelist(HttpRequestMessage request, int doctorid, DateTime? datetime = null)

        {
            List<tbScheduleData> result = dbContext.tbScheduleDatas.Where(a => a.DoctorID == doctorid && a.AppointmentDatetime.Value.Day == datetime.Value.Day).ToList(); //distinct by fromtime to time
            return request.CreateResponse<List<tbScheduleData>>(HttpStatusCode.OK, result);
        }



        [HttpGet]
        [Route("api/appointment/doctorlist")]
        public HttpResponseMessage DoctorList(HttpRequestMessage request, int hospitalid = 2, DateTime? datetime = null)
        {
            //List<tbDoctorHospital> doctors = dbContext.tbDoctorHospitals.Where(a => a.IsDeleted != true && a.HospitalID == hospitalid).ToList();
            //return request.CreateResponse<List<tbDoctorHospital>>(HttpStatusCode.OK, doctors);
            var result = (from sch in dbContext.tbScheduleDatas.Where(a => a.IsDeleted != true && a.AppointmentDatetime.Value.Day == datetime.Value.Day && a.HospitalID == hospitalid)
                          join doc in dbContext.tbDoctors.Where(a => a.IsDeleted != true)
                          on sch.DoctorID equals doc.ID
                          select doc).DistinctBy(a => a.Name).ToList();
            return request.CreateResponse<List<tbDoctor>>(HttpStatusCode.OK, result);
        }



        //    [Route("api/appointment/waitinglist")]
        //[HttpGet]
        //public HttpResponseMessage waitinglist(HttpRequestMessage request, string name = null, string status = null, int pagesize = 10, int page = 1)
        //{
        //    Expression<Func<tbAppointment, bool>> namefilter, statusfilter = null;
        //    if (name != null)
        //    {
        //        namefilter = l => l.PatientName.Contains(name);
        //    }
        //    else
        //    {
        //        namefilter = l => l.IsDeleted != true;
        //    }
        //    if (status != null)
        //    {
        //        statusfilter = l => l.Status == status;
        //    }
        //    else
        //    {
        //        statusfilter = l => l.IsDeleted != true;
        //    }
        //    IQueryable<tbAppointment> result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status == "WAITING").Where(namefilter).Where(statusfilter);
        //    var totalCount = result.Count();
        //    var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
        //    var dataList = result.OrderBy(a => a.PatientName)
        //                 .Skip(pagesize * (page - 1)).Take(pagesize);
        //    PagedListServer<tbAppointment> model = new PagedListServer<tbAppointment>();
        //    model.Results = dataList.ToList();
        //    model.TotalCount = totalCount;
        //    model.TotalPages = totalpages;
        //    dbContext.Dispose();
        //    return request.CreateResponse<PagedListServer<tbAppointment>>(HttpStatusCode.OK, model);
        //}


        //[Route("api/appointment/historylist")]
        //[HttpGet]
        //public HttpResponseMessage historylist(HttpRequestMessage request, string name = null, string status = null, int pagesize = 10, int page = 1)
        //{
        //    Expression<Func<tbAppointment, bool>> namefilter,statusfilter = null;
        //    if (name != null)
        //    {
        //        namefilter = l => l.PatientName.Contains(name);
        //    }
        //    else
        //    {
        //        namefilter = l => l.IsDeleted != true;
        //    }
        //    if (status != null)
        //    {
        //        statusfilter = l => l.Status == status;
        //    }
        //    else
        //    {
        //        statusfilter = l => l.IsDeleted != true;
        //    }
        //    IQueryable<tbAppointment> result = appointmentRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.Status != "ACCEPTED" && a.Status != "CHECKIN" && a.Status != null).Where(namefilter).Where(statusfilter);
        //    var totalCount = result.Count();
        //    var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
        //    var dataList = result.OrderBy(a => a.PatientName)
        //                 .Skip(pagesize * (page - 1)).Take(pagesize);
        //    PagedListServer<tbAppointment> model = new PagedListServer<tbAppointment>();
        //    model.Results = dataList.ToList();
        //    model.TotalCount = totalCount;
        //    model.TotalPages = totalpages;
        //    dbContext.Dispose();
        //    return request.CreateResponse<PagedListServer<tbAppointment>>(HttpStatusCode.OK, model);
        //}

    }
}
