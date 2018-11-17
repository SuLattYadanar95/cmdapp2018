using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class tbScheduleData
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> AppointmentDatetime { get; set; }
        public string AppointmentTime { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public string DoctorName { get; set; }
        public Nullable<int> HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string Day { get; set; }
        public Nullable<int> MaxPatientCount { get; set; }
        public Nullable<System.DateTime> Fromtime { get; set; }
        public Nullable<System.DateTime> Totime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<bool> IsCancelled { get; set; }
        public Nullable<int> ReachedPatientCount { get; set; }
        public Nullable<int> ScheduleID { get; set; }

        public Nullable<bool> IsStopped { get; set; }
        public Nullable<bool> IsLimited { get; set; }
        public Nullable<int> DefaultPatientCount { get; set; }


    }
}
