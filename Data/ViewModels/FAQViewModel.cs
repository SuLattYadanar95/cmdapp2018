using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
namespace Data.ViewModels
{
    [Serializable]
    public class FAQViewModel
    {
        public string DomainTitle { get; set; }
        public tbService Service { get; set; }
        public tbProcedure Procedure { get; set; }
        public List<tbFAQ> FAQs { get; set; }
    }
}
