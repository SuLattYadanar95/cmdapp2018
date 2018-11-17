using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbShopCategoryItemMap : EntityTypeConfiguration<tbShopCategoryItem>
    {
        public tbShopCategoryItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ItemCode)
                .HasMaxLength(255);

            this.Property(t => t.CategoryCode)
                .HasMaxLength(255);

            this.Property(t => t.ShopCode)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("tbShopCategoryItem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.ItemTitle).HasColumnName("ItemTitle");
            this.Property(t => t.CetegoryID).HasColumnName("CetegoryID");
            this.Property(t => t.CategoryTitle).HasColumnName("CategoryTitle");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.SubCategoryID).HasColumnName("SubCategoryID");
            this.Property(t => t.SubCategoryTitle).HasColumnName("SubCategoryTitle");
            this.Property(t => t.ShopID).HasColumnName("ShopID");
            this.Property(t => t.ShopTitle).HasColumnName("ShopTitle");
            this.Property(t => t.ShopCode).HasColumnName("ShopCode");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
        }
    }
}
