using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbCategoryMap : EntityTypeConfiguration<tbCategory>
    {
        public tbCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("tbCategory");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.IsAvailable).HasColumnName("IsAvailable");
        }
    }
}
