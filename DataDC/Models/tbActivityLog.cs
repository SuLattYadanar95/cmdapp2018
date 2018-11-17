using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbActivityLog
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string EventPayload { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string UserName { get; set; }
        public string UserGender { get; set; }
        public string UserProfilePic { get; set; }
        public string Type { get; set; }
        public string TemplateType { get; set; }
    }
}
