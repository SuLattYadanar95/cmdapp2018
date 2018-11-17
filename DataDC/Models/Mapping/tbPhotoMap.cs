using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbPhotoMap : EntityTypeConfiguration<tbPhoto>
    {
        public tbPhotoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.MemberId)
                .HasMaxLength(50);

            this.Property(t => t.PhotoUrl)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbPhoto");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MemberId).HasColumnName("MemberId");
            this.Property(t => t.ShopId).HasColumnName("ShopId");
            this.Property(t => t.PhotoUrl).HasColumnName("PhotoUrl");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
        }
    }
}
