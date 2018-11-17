using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbImage
    {
        public int ID { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string Url { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
