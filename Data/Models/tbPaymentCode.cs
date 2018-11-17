using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class tbPaymentCode
    {
        public string Code { get; set; }
        public Nullable<bool> IsRedeemed { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public string VoucherCode { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public int ID { get; set; }
        public string MerchantName { get; set; }
        public Nullable<System.DateTime> GeneratedAt { get; set; }
        public Nullable<int> ShopId { get; set; }
        public Nullable<System.DateTime> RedeemedAt { get; set; }
        public Nullable<decimal> TotalFees { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public string UserId { get; set; }
        public Nullable<int> MerchantID { get; set; }
        public string Description { get; set; }
    }
}
