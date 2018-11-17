using Data.Helper;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public partial class tbProcedure
    {
        public int ID { get; set; }
        public Nullable<int> ServiceID { get; set; }
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
        public Nullable<int> StepIndex { get; set; }
        public Nullable<bool> IsMandatory { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public string ServiceTitle { get; set; }
        public string Image { get; set; }
        public string Code { get; set; }
        public string AttachmentUrls { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public string BodyHTML { get; set; }
        public string ImageUrl { get { return string.Format("https://kktstroage.azureedge.net/yammo/smib/procedure/{0}", Image ?? "placeholder.jpg"); } }
    }
}
