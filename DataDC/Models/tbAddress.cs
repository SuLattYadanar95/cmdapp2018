using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbAddress
    {
        public int ID { get; set; }
        public string CustomerId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Township { get; set; }
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Zip { get; set; }
        public string Type { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public string CountryCode { get; set; }
        public System.Data.Entity.Spatial.DbGeography Geolocation { get; set; }
    }
}
