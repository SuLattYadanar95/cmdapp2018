using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbOrderMap : EntityTypeConfiguration<tbOrder>
    {
        public tbOrderMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CustomerId)
                .HasMaxLength(50);

            this.Property(t => t.ShopName)
                .HasMaxLength(50);

            this.Property(t => t.CustomerName)
                .HasMaxLength(50);

            this.Property(t => t.VoucherCode)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.Condition)
                .HasMaxLength(50);

            this.Property(t => t.Currency)
                .HasMaxLength(50);

            this.Property(t => t.CancelReason)
                .HasMaxLength(50);

            this.Property(t => t.AppId)
                .HasMaxLength(50);

            this.Property(t => t.Tags)
                .HasMaxLength(50);

            this.Property(t => t.FinancialStatus)
                .HasMaxLength(50);

            this.Property(t => t.DiscountCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbOrder");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.ShopName).HasColumnName("ShopName");
            this.Property(t => t.ShopId).HasColumnName("ShopId");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.VoucherCode).HasColumnName("VoucherCode");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.TotalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.ShippingAddressId).HasColumnName("ShippingAddressId");
            this.Property(t => t.BillingAddressId).HasColumnName("BillingAddressId");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.CreatedAt).HasColumnName("CreatedAt");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ProcessingMethod).HasColumnName("ProcessingMethod");
            this.Property(t => t.ServiceFees).HasColumnName("ServiceFees");
            this.Property(t => t.DeliveryServiceFees).HasColumnName("DeliveryServiceFees");
            this.Property(t => t.Condition).HasColumnName("Condition");
            this.Property(t => t.SubTotalPrice).HasColumnName("SubTotalPrice");
            this.Property(t => t.TotalWeight).HasColumnName("TotalWeight");
            this.Property(t => t.TotalTax).HasColumnName("TotalTax");
            this.Property(t => t.TaxIncluded).HasColumnName("TaxIncluded");
            this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.TotalDiscounts).HasColumnName("TotalDiscounts");
            this.Property(t => t.CancelledAt).HasColumnName("CancelledAt");
            this.Property(t => t.CancelReason).HasColumnName("CancelReason");
            this.Property(t => t.ProcessedAt).HasColumnName("ProcessedAt");
            this.Property(t => t.AppId).HasColumnName("AppId");
            this.Property(t => t.Tags).HasColumnName("Tags");
            this.Property(t => t.FullfilledAt).HasColumnName("FullfilledAt");
            this.Property(t => t.TotalItemPrice).HasColumnName("TotalItemPrice");
            this.Property(t => t.FinancialStatus).HasColumnName("FinancialStatus");
            this.Property(t => t.DiscountCode).HasColumnName("DiscountCode");
            this.Property(t => t.CheckOutId).HasColumnName("CheckOutId");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Distance).HasColumnName("Distance");
            this.Property(t => t.DistancePrice).HasColumnName("DistancePrice");
            this.Property(t => t.IsConfirmed).HasColumnName("IsConfirmed");
            this.Property(t => t.IsConfirmedDate).HasColumnName("IsConfirmedDate");
            this.Property(t => t.InProcess).HasColumnName("InProcess");
            this.Property(t => t.InProcessDate).HasColumnName("InProcessDate");
            this.Property(t => t.IsDelivered).HasColumnName("IsDelivered");
            this.Property(t => t.IsDeliveredDate).HasColumnName("IsDeliveredDate");
        }
    }
}
