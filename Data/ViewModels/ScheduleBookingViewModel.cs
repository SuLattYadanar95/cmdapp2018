using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class ScheduleBookingViewModel
    {
        public tbDoctor doctor { get; set; }
        public List<tbScheduleData> schedule { get; set; }
    }

    public class AppointScheduleViewModel
    {
        public DateTime? appointDatetime { get; set; }
        public List<tbScheduleData> scheduledatas { get; set; }
    }

    public class PatientAppointmentViewModel
    {
        public tbPatient patient { get; set; }
        public tbAppointment appointment { get; set; }
    }
}

