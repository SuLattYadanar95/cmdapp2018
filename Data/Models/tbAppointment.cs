using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class tbAppointment
    {
        public int ID { get; set; }
        public Nullable<int> PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientName_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(PatientName))
                {
                    return MMFontHelper.Uni2ZG(PatientName);
                }
                return string.Empty;
            }
        }
        public Nullable<int> PatientAge { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorName_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(DoctorName))
                {
                    return MMFontHelper.Uni2ZG(DoctorName);
                }
                return string.Empty;
            }
        }
        public Nullable<int> HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalName_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(HospitalName))
                {
                    return MMFontHelper.Uni2ZG(HospitalName);
                }
                return string.Empty;
            }
        }
        public string Status { get; set; }
        public Nullable<System.DateTime> AppointmentDateTime { get; set; }
        public int Counter { get; set; }
        public Nullable<bool> IsWaiting { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> ScheduleID { get; set; }
        public string Day { get; set; }

        public DateTime? Accesstime { get; set; }
        public Nullable<bool> IsEmergency { get; set; }
        public Nullable<bool> IsEmergencyApproved { get; set; }

        public string Problem { get; set; }
        public string Problem_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Problem))
                {
                    return MMFontHelper.Uni2ZG(Problem);
                }
                return string.Empty;
            }
        }

        public string BookingType { get; set; }
        public Nullable<bool> IsCheckIn { get; set; }
        public Nullable<bool> IsDelByAdmin { get; set; }
        public Nullable<int> ScheduleDataID { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<int> SkipCount { get; set; }
        public Nullable<int> Position { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Gender { get; set; }
    }
}
