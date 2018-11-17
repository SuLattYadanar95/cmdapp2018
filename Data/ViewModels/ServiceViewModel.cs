using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class ServiceViewModel
    {
        public string DomainTitle { get; set; }
        public tbService Service { get; set; }
        public List<tbProcedure> Procedures { get; set; }
    }
}
