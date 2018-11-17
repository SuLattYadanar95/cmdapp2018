using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class Store
    {
        public Nullable<short> ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public bool Active { get; set; }
        public Nullable<double> Createdby { get; set; }
        public string Township { get; set; }
        public Nullable<double> CodeIndex { get; set; }
        public bool IsDeleted { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Baseboardserial { get; set; }
        public string AgencyPhno { get; set; }
        public string Agencympin { get; set; }
        public string AgencyCode { get; set; }
        public Nullable<double> AreaManagerId { get; set; }
        public string AreaManagerName { get; set; }
        public string CreatedbyName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Machineguid { get; set; }
    }
}
