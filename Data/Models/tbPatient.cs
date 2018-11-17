using Data.Helper;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbPatient
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
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string Problem { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserType { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string MsgrID { get; set; }
        public string MsgrName { get; set; }
    }
}
