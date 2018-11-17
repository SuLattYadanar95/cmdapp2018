using CareMeMobileApi.Repository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeMobileApi.Controllers
{
    public class BookingController : ApiController
    {
        CaremeDBContext dbContext;

        private ScheduleRepository scheduleRepo = null;
        private HospitalRepository hospitalRepo = null;
        private DoctorRepository doctorRepo = null;
        private AppointmentRepository appointmentRepo = null;

        public BookingController()
        {
            dbContext = new CaremeDBContext();
            scheduleRepo = new ScheduleRepository(dbContext);
            hospitalRepo = new HospitalRepository(dbContext);
            doctorRepo = new DoctorRepository(dbContext);
            appointmentRepo = new AppointmentRepository(dbContext);
        }


    }
}
