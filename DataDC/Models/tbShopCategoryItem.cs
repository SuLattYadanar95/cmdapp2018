using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbShopCategoryItem
    {
        public int ID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemTitle { get; set; }
        public Nullable<int> CetegoryID { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryCode { get; set; }
        public Nullable<int> SubCategoryID { get; set; }
        public string SubCategoryTitle { get; set; }
        public Nullable<int> ShopID { get; set; }
        public string ShopTitle { get; set; }
        public string ShopCode { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
    }
}
