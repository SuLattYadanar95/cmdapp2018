using Data.Helper;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public partial class tbService
    {
        public int ID { get; set; }
        public string Title { get; set; }
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
        public string Tag { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<int> DomainId { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ImageUrl { get { return string.Format("https://kktstroage.azureedge.net/yammo/airticket/service/{0}", Image ?? "placeholder.png"); } }
        public int? HospitalId { get; set; }
        public string Phone { get; set; }
    }
}
