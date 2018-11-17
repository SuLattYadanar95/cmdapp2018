using Data.Helper;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class DoctorHospitalViewModel
    {
        public tbDoctor doctor { get; set; }
        public tbDoctorHospital hospital { get; set; }
    }

    public class ScheduleHospitalViewModel
    {
        public tbHospital hospital { get; set; }
        public List<tbSchedule> schedules { get; set; }
    }

    public class NotiTimeFrameViewModel
    {
        public List<NotiViewModel> NotiList { get; set; }
        public string Timeframe { get; set; }
    }

    public class NotiViewModel
    {
        public int hospitalID { get; set; }
        public string hospitalName { get; set; }
        public string hospitalName_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(hospitalName))
                {
                    return MMFontHelper.Uni2ZG(hospitalName);
                }
                return string.Empty;
            }
        }
        public int patientCount { get; set; }
        public int scheduleID { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime LastActivtBookedTime { get; set; }
        public int MaxPatientCount { get; set; }
        public Nullable<bool> IsStopped { get; set; }
        public Nullable<bool> IsLimited { get; set; }
    }
}
