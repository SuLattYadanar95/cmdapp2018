using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class HospitalSchedules
    {
        public tbHospital tbHospital { get; set; }
        public List<tbScheduleData> scheduleDataList { get; set; }
    }
}
