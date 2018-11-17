using Data.Helper;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.ViewModels
{
    public class DoctorHomeViewModel
    {     
        public List<CountInTimeFrame> CountList { get; set; }
        public string doctorName { get; set; }
        public string doctorName_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(doctorName))
                {
                    return MMFontHelper.Uni2ZG(doctorName);
                }
                return string.Empty;
            }
        }
        public int doctorid { get; set; }
        
    }

    public class HospitalListViewModel
    {
        public int patientCount { get; set; }
        public int? hospitalId { get; set; }
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
        public DateTime? fromTime { get; set; }
        public DateTime? toTime { get; set;}
        public int? scheduleID { get; set; }
        public DateTime? appointmentDateTime { get; set; }

    }

    public class CountInTimeFrame
    {
        public List<HospitalListViewModel> hospitalList { get; set; }
        public string TimeFrame { get; set; }
        public int totalPatientCount { get; set; }

    }

    public class PatientViewModel
    {
        //public string patientName { get; set; }
        //public int? patientAge { get; set; }
        //public int? counter { get; set; }
        //public string problem { get; set; }

        public tbAppointment appointment { get; set; }
        public tbPatient patient { get; set; }


    }
}