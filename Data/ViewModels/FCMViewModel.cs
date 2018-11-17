using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class FCMViewModel
    {
        public string to { get; set; }
        public fcmdata data { get; set; }
    }

    public class fcmdata
    {
        public string title { get; set; }
        public string body { get; set; }
        public int doctorId { get; set; }
        public string type { get; set; }
    }
}
