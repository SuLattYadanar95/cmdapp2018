using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.ViewModels
{
    public class ScheduleDoctorViewModel
    {
        public tbDoctorHospital dochos { get; set; }
        public tbDoctor doctor { get; set; }
        public List<tbScheduleData> schedule { get; set; }
        public int schedulecount { get; set; }
    }


}