using Data.Helper;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbSchedule
    {
        public int ID { get; set; }
        public Nullable<int> HospitalID { get; set; }
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
        public Nullable<System.DateTime> Fromtime { get; set; }
        public Nullable<System.DateTime> Totime { get; set; }
        public Nullable<int> DoctorID { get; set; }
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
        public Nullable<bool> IsSunday { get; set; }
        public Nullable<bool> IsMonday { get; set; }
        public Nullable<bool> IsTuesday { get; set; }
        public Nullable<bool> IsWednesday { get; set; }
        public Nullable<bool> IsThursday { get; set; }
        public Nullable<bool> IsFriday { get; set; }
        public Nullable<bool> IsSaturday { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<int> PatientLimit { get; set; }
    }
}
