using System;
using System.Collections.Generic;

namespace DataDC.Models
{
    public partial class tbOrder
    {
        public int ID { get; set; }
        public string CustomerId { get; set; }
        public string ShopName { get; set; }
        public Nullable<int> ShopId { get; set; }
        public string CustomerName { get; set; }
        public string VoucherCode { get; set; }
        public Nullable<System.DateTime> Accesstime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<int> ShippingAddressId { get; set; }
        public Nullable<int> BillingAddressId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string Status { get; set; }
        public string ProcessingMethod { get; set; }
        public Nullable<decimal> ServiceFees { get; set; }
        public Nullable<decimal> DeliveryServiceFees { get; set; }
        public string Condition { get; set; }
        public Nullable<decimal> SubTotalPrice { get; set; }
        public Nullable<decimal> TotalWeight { get; set; }
        public Nullable<decimal> TotalTax { get; set; }
        public Nullable<bool> TaxIncluded { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> TotalDiscounts { get; set; }
        public Nullable<System.DateTime> CancelledAt { get; set; }
        public string CancelReason { get; set; }
        public Nullable<System.DateTime> ProcessedAt { get; set; }
        public string AppId { get; set; }
        public string Tags { get; set; }
        public Nullable<System.DateTime> FullfilledAt { get; set; }
        public Nullable<decimal> TotalItemPrice { get; set; }
        public string FinancialStatus { get; set; }
        public string DiscountCode { get; set; }
        public Nullable<int> CheckOutId { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> Distance { get; set; }
        public Nullable<decimal> DistancePrice { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<System.DateTime> IsConfirmedDate { get; set; }
        public Nullable<bool> InProcess { get; set; }
        public Nullable<System.DateTime> InProcessDate { get; set; }
        public Nullable<bool> IsDelivered { get; set; }
        public Nullable<System.DateTime> IsDeliveredDate { get; set; }
    }
}
