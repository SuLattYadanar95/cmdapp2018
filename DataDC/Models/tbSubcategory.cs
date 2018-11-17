using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbSubcategory
    {
        public Nullable<int> CetegoryID { get; set; }
        public string CategoryTitle { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public string Tags { get; set; }
    }
}
