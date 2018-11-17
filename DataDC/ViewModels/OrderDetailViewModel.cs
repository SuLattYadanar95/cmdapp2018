using Data.Models;
using DataDC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDC.ViewModels
{
    public class OrderWithDetailsViewModel
    {
        public tbOrder order { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public List<OrderDetailViewModel> orderdetails { get; set; }
       
    }

    public class OrderDetailViewModel
    {
        public int ID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string CategoryName { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<int> ItemQty { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<decimal> TotalPrice { get; set; }
        public string VoucherCode { get; set; }
       
        public string ShopName { get; set; }
        public string ItemPhoto { get; set; }
        public Nullable<int> ShopID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string ItemPhotoUrl {
            get
            {
                if (ItemPhoto != null)
                {
                    return string.Format("https://portalvhdslvb28rs1c3tmc.blob.core.windows.net/yammo/foody/item/{0}", ItemPhoto);
                }
                else
                {
                    return "https://portalvhdslvb28rs1c3tmc.blob.core.windows.net/yammo/foody/ygn_food_mart_logo.png";
                }
            }
        }
      

    }
}
