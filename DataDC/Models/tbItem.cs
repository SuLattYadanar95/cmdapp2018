using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbItem
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string UOM { get; set; }
        public string SKU { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<System.DateTime> PublishedAt { get; set; }
        public string PublishedScope { get; set; }
        public bool IsDeleted { get; set; }
        public string Photo { get; set; }
        public string BodyHtml { get; set; }
        public Nullable<bool> IsPromoted { get; set; }
        public string Tags { get; set; }
    }
}
