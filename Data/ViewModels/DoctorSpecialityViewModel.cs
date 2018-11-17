using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    [DataContract]
    public class DoctorSpecialityViewModel
    {
        [DataMember]
        public tbDoctor doctor { get; set; }
        [DataMember]
        public tbSpecialty specialty { get; set; }
        [DataMember]
        public int hospitalid { get; set; }
        [DataMember]
        public string hospitalname { get; set; }
    }

    public class DoctorScheduleViewModel
    {
        public tbDoctorHospital dochos { get; set; }
        public List<tbSchedule> schedules { get; set; }
    }

    public class DoctorSpecialitiesViewModel
    {
        public List<tbDoctor> doctors { get; set; }
        public List<tbSpecialty> specialities { get; set; }
    }
}
