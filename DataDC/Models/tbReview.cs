using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbReview
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Nullable<int> ShopId { get; set; }
        public string MemberId { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsReported { get; set; }
    }
}
