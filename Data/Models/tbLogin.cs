using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbLogin
    {
        public int ID { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public Nullable<System.DateTime> LogoutTime { get; set; }
        public string Role { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ShopName { get; set; }
        public Nullable<int> ShopId { get; set; }
        public string StaffName { get; set; }
    }
}
