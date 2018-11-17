using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDC.ViewModels
{
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Phone { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd, MMM yyyy}", ApplyFormatInEditMode = true)]
        public System.Nullable<DateTime> SendDate { get; set; }
    }
}
