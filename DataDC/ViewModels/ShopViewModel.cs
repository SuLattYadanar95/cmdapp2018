using Data.Models;
using DataDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDC.ViewModels
{
    public class ShopViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Township { get; set; }
        public string ContactPhone { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string ContactEmail { get; set; }
        public Nullable<System.DateTime> OpeningHours { get; set; }
        public Nullable<System.DateTime> ClosingHours { get; set; }
        public string Tags { get; set; }
        public string CategoryName { get; set; }
        public bool LocationValidity { get; set; }
        public Nullable<bool> IsPromoted { get; set; }
        public string PhotoUrl
        {
            get
            {
                if (Photo != null)
                {
                    return string.Format("https://portalvhdslvb28rs1c3tmc.blob.core.windows.net/yammo/foody/shop/{0}", Photo);
                }
                else
                {
                    return "https://portalvhdslvb28rs1c3tmc.blob.core.windows.net/yammo/foody/ygn_food_mart_logo.png";
                }
            }
        }
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
        public int? CategoryId { get; set; }
        public string Title { get; set; }


    }

    public class ShopCategoriesItemsViewModel
    {
        public tbShop Shop { get; set; }
        public List<CategoryViewModel> CategoryWithItems { get; set; }
    }

    
}
