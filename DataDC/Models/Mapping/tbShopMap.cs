using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbShopMap : EntityTypeConfiguration<tbShop>
    {
        public tbShopMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CategoryName)
                .HasMaxLength(255);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbShop");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.ContactEmail).HasColumnName("ContactEmail");
            this.Property(t => t.OpeningHours).HasColumnName("OpeningHours");
            this.Property(t => t.ClosingHours).HasColumnName("ClosingHours");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.LocationValidity).HasColumnName("LocationValidity");
            this.Property(t => t.IsPromoted).HasColumnName("IsPromoted");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.RateCount).HasColumnName("RateCount");
            this.Property(t => t.ReviewCount).HasColumnName("ReviewCount");
            this.Property(t => t.AverageRate).HasColumnName("AverageRate");
            this.Property(t => t.LastReview).HasColumnName("LastReview");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.IsEditorChoice).HasColumnName("IsEditorChoice");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CodeIndex).HasColumnName("CodeIndex");
            this.Property(t => t.ShopCategoryId).HasColumnName("ShopCategoryId");
            this.Property(t => t.ShopCategoryName).HasColumnName("ShopCategoryName");
            this.Property(t => t.ShopCategoryGUID).HasColumnName("ShopCategoryGUID");
        }
    }
}
