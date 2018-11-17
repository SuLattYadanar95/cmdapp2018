using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbRate
    {
        public int ID { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<int> ShopID { get; set; }
        public string MemberID { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
    }
}
