using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbOrderDetail
    {
        public int ID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> MenuID { get; set; }
        public string MenuName { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<int> ItemQty { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public string VoucherCode { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public string ShopName { get; set; }
    }
}
