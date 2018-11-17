using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbDoctorHospital
    {
        public int ID { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public string DoctorName { get; set; }
        public Nullable<int> HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string AvailableTime { get; set; }
        public string AvailableDay { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
    }
}
