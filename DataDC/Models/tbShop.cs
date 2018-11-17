using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbShop
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Township { get; set; }
        public string ContactPhone { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string ContactEmail { get; set; }
        public Nullable<System.DateTime> OpeningHours { get; set; }
        public Nullable<System.DateTime> ClosingHours { get; set; }
        public string Tag { get; set; }
        public string CategoryName { get; set; }
        public bool LocationValidity { get; set; }
        public Nullable<bool> IsPromoted { get; set; }
        public string Photo { get; set; }
        public Nullable<int> RateCount { get; set; }
        public Nullable<int> ReviewCount { get; set; }
        public Nullable<int> AverageRate { get; set; }
        public string LastReview { get; set; }
        public string Website { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public bool IsDeleted { get; set; }
        public string GroupName { get; set; }
        public Nullable<bool> IsEditorChoice { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Code { get; set; }
        public Nullable<int> CodeIndex { get; set; }
        public Nullable<int> ShopCategoryId { get; set; }
        public string ShopCategoryName { get; set; }
        public Nullable<System.Guid> ShopCategoryGUID { get; set; }
    }
}
