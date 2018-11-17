using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbCustomer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string Note { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<bool> AcceptMarketing { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
