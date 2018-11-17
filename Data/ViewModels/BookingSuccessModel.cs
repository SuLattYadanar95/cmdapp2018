using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class BookingSuccessModel
    {
        public DoctorSpecialityViewModel doctor { get; set; }
        public tbHospital hospital { get; set; }
        public PatientAppointmentViewModel pavm { get; set; }
        public tbScheduleData scheduleData { get; set; }
    }
}
