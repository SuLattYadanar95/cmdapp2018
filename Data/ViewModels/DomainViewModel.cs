using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class DomainViewModel
    {
        public tbDomain Domain { get; set; }
        public List<ServiceViewModel> ServiceInfo { get; set; }
    }
    public class DomainWithServiceViewModel
    {
        public tbDomain Domain { get; set; }
        public List<tbService> Services { get; set; }
    }
}
