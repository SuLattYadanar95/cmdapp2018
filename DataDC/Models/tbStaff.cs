using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbStaff
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string State { get; set; }
        public string Township { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Role { get; set; }
        public string Qualification { get; set; }
        public Nullable<int> CodeIndex { get; set; }
        public Nullable<int> ShopId { get; set; }
        public string JobTitle { get; set; }
        public string ShopName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<System.Guid> UniqueID { get; set; }
        public string UserId { get; set; }
    }
}
