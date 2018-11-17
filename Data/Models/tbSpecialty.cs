using System;
using System.Collections.Generic;
using Data.Helper;

namespace Data.Models
{
    public partial class tbSpecialty
    {
        public int ID { get; set; }
        public string Specialty { get; set; }
        public string Specialty_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Specialty))
                {
                    return MMFontHelper.Uni2ZG(Specialty);
                }
                return string.Empty;
            }
        }
        public string Description { get; set; }
        public string Symptom_English { get; set; }
        public string Symptom_Myanmar { get; set; }
        public string Symptom_Myanmar_ZG
        {
            get
            {
                if (!string.IsNullOrEmpty(Symptom_Myanmar))
                {
                    return MMFontHelper.Uni2ZG(Symptom_Myanmar);
                }
                return string.Empty;
            }
        }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
    }
}
