using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbPaymentCodeMap : EntityTypeConfiguration<tbPaymentCode>
    {
        public tbPaymentCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(255);

            this.Property(t => t.Type)
                .HasMaxLength(255);

            this.Property(t => t.VoucherCode)
                .HasMaxLength(255);

            this.Property(t => t.UserId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbPaymentCode");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.IsRedeemed).HasColumnName("IsRedeemed");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.VoucherCode).HasColumnName("VoucherCode");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ValidFrom).HasColumnName("ValidFrom");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MerchantName).HasColumnName("MerchantName");
            this.Property(t => t.GeneratedAt).HasColumnName("GeneratedAt");
            this.Property(t => t.ShopId).HasColumnName("ShopId");
            this.Property(t => t.RedeemedAt).HasColumnName("RedeemedAt");
            this.Property(t => t.TotalFees).HasColumnName("TotalFees");
            this.Property(t => t.Tax).HasColumnName("Tax");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.MerchantID).HasColumnName("MerchantID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
