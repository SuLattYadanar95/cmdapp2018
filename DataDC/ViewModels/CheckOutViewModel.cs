using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDC.ViewModels
{
    public class CheckOutViewModel
    {
        public string ItemNQty { get; set; }
        public string CustomerName { get; set; }
        //public tbOrder tbOrder { get; set; }
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string Phone { get; set; }
        public string Remark { get; set; }
        public string DiscountCode { get; set; }
        public decimal TotalItemPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Distance { get; set; }
        public decimal DistancePrice { get; set; }
        public string ShippingAddress { get; set; }
        public string AppId { get; set; }
        public decimal? lat { get; set; }
        public decimal? lng { get; set; }
         
    }
}
