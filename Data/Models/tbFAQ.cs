using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Serializable]
    public partial class tbFAQ
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AnswerHTML { get; set; }
        public Nullable<int> ProcedureId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }       
        public Nullable<bool> IsPublished { get; set; }
       
    }
}
