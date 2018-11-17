using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbSubcategoryMap : EntityTypeConfiguration<tbSubcategory>
    {
        public tbSubcategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("tbSubcategory");
            this.Property(t => t.CetegoryID).HasColumnName("CetegoryID");
            this.Property(t => t.CategoryTitle).HasColumnName("CategoryTitle");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Tags).HasColumnName("Tags");
        }
    }
}
