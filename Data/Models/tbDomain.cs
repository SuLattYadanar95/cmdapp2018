using Data.Helper;
using System;
using System.Collections.Generic;

namespace Data.Models
{
   [Serializable]
    public partial class tbDomain
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Description_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Description))
                {
                    return MMFontHelper.Uni2ZG(Description);
                }
                return string.Empty;
            }
        }
        public string BodyHtml { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public string Tags { get; set; }
        public string Action { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get { return string.Format("https://kktstroage.azureedge.net/yammo/airticket/{0}", Image ?? "placeholder.png"); } }
        public Nullable<int> HospitalId { get; set; }
    }
}
