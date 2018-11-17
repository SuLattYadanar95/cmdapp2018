using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbOrderDetailMap : EntityTypeConfiguration<tbOrderDetail>
    {
        public tbOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.VoucherCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbOrderDetail");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.MenuID).HasColumnName("MenuID");
            this.Property(t => t.MenuName).HasColumnName("MenuName");
            this.Property(t => t.ItemPrice).HasColumnName("ItemPrice");
            this.Property(t => t.ItemQty).HasColumnName("ItemQty");
            this.Property(t => t.TotalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.VoucherCode).HasColumnName("VoucherCode");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.ShopID).HasColumnName("ShopID");
            this.Property(t => t.ShopName).HasColumnName("ShopName");
        }
    }
}
