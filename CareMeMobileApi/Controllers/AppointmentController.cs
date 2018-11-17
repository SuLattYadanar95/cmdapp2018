using CareMeMobileApi.Repository;
using Data.Helper;
using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeMobileApi.Controllers
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
        [HttpGet]
        [Route("api/appointment/changestatus")]
        public HttpResponseMessage changestatus(HttpRequestMessage request, int id, string status)
        {
            bool result = false;
            tbAppointment appointment = appointmentRepo.Get().Where(a => a.ID == id).FirstOrDefault();
            if (appointment != null)
            {
                if (status == "BOOKED")
                {
                    appointment.Status = null;
                    appointment.IsWaiting = true;
                    appointment.IsApproved = true;
                }
                else if (status == "WAITING")
                {
                    appointment.Status = status;
                    appointment.IsWaiting = true; //updated
                    
                }
                else if (status == "CHECKIN")
                {
                    appointment.IsWaiting = true;
                    appointment.IsCheckIn = true;
                    appointment.Status = status;
                }
                else if (status == "CHECKOUT")
                {
                    appointment.IsWaiting = true;
                    appointment.IsCheckIn = true;
                    appointment.Status = status;
                }
                else if (status == "CANCEL")
                {
                    appointment.Status = "CANCEL";
                    appointment.IsDelByAdmin = true;
                    appointment.IsWaiting = false;
                    appointment.IsCheckIn = false;
                }
                else if (status == "SKIP")
                {
                    if (appointment.SkipCount == null)
                    {
                        appointment.SkipCount = 1;
                        appointment.Position += 5;
                        appointment.CreatedTime = DateTime.UtcNow.getLocalTime();
                    }
                    else
                    {
                        appointment.Status = "CANCEL";
                        appointment.IsDelByAdmin = true;
                    }
                }

                result = appointmentRepo.UpdatewithObj(appointment) != null ? true : false;
            }
            return request.CreateResponse<bool>(HttpStatusCode.OK, result);
          
        }
    }
}
