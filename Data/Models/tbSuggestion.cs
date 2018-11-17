using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class tbSuggestion
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Code { get; set; }
        public Nullable<int> CodeIndex { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> RespondedAt { get; set; }
        public string Phone { get; set; }
        public string OperationType { get; set; }
        public string MessengerPhone
        {
            get
            {
                if (Phone != null)
                {
                    return string.Format("+95{0}", Phone.getCleanedNumber().TrimStart(new char[] { '0' }));
                }
                return null;
            }
        }
        public Nullable<bool> IsReponded { get; set; }
        public Nullable<int> ManagedbyId { get; set; }
        public string ManagedbyName { get; set; }
    }
}
