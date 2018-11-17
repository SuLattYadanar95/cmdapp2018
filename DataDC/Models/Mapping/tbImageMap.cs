using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbImageMap : EntityTypeConfiguration<tbImage>
    {
        public tbImageMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Url)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbImage");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ItemId).HasColumnName("ItemId");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.CreatedAt).HasColumnName("CreatedAt");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
