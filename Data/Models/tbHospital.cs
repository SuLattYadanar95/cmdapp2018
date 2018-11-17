using Data.Helper;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbHospital
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
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Address_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Address))
                {
                    return MMFontHelper.Uni2ZG(Address);
                }
                return string.Empty;
            }
        }
        public Nullable<int> Latitude { get; set; }
        public Nullable<int> Longitude { get; set; }
        public string Township { get; set; }
        public string Township_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Township))
                {
                    return MMFontHelper.Uni2ZG(Township);
                }
                return string.Empty;
            }
        }

        public string State { get; set; }
        public string State_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(State))
                {
                    return MMFontHelper.Uni2ZG(State);
                }
                return string.Empty;
            }
        }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Photo { get; set; }
        public string Specialty { get; set; }
        public string SpecialtyMyan { get; set; }
        public string WelcomePhoto { get; set; }
        public string PhotoUrl
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("https://kktstroage.azureedge.net/yammo/careme/{0}", Photo);
                }
                else
                {
                    return "https://kktstroage.azureedge.net/yammo/careme/logo_rounded.png";
                }
            }
        }
        public string WelcomePhotoUrl
        {
            get
            {
                if (this.WelcomePhoto != null)
                {
                    return string.Format("https://kktstroage.azureedge.net/yammo/careme/{0}", WelcomePhoto);
                }
                else
                {
                    return "https://kktstroage.azureedge.net/yammo/careme/logo_rounded.png";
                }
            }
        }
    }
}
