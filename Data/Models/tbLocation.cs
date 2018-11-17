using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbLocation
    {
        public int ID { get; set; }
        public string DialingCode { get; set; }
        public string Country { get; set; }
        public string StateDivision { get; set; }
        public string DivisionCode { get; set; }
        public string Township { get; set; }
        public string TownshipCode { get; set; }
        public string Createdby { get; set; }
        public string Accesstime { get; set; }
        public string Abbreviation { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
