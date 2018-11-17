using Data.Helper;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbDoctor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Name_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    return MMFontHelper.Uni2ZG(Name);
                }
                return string.Empty;
            }
        }
        public string Specialty { get; set; }
        public string SpecialtyMyan { get; set; }
        public string SpecialtyMyan_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Specialty))
                {
                    return MMFontHelper.Uni2ZG(Specialty);
                }
                return string.Empty;
            }
        }
        public string Qualification { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<int> LicenseID { get; set; }
        public Nullable<bool> IsSpecialist { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Pin { get; set; }
        public Nullable<int> SpecialityID { get; set; }
        public string Photo { get; set; }
        public string PhotoUrl {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("https://kktstroage.azureedge.net/yammo/careme/{0}", Photo);
                }
                else
                {
                    return "https://kktstroage.azureedge.net/yammo/careme/default_doctor_photo.jpg";
                }
            }
        }
     

        public string SystemStatus { get; set; }
        public string UserToken { get; set; }
    }
}
