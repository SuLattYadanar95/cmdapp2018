using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public string Code { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
    }
}
