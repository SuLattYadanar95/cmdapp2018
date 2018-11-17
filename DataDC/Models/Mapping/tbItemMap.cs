using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbItemMap : EntityTypeConfiguration<tbItem>
    {
        public tbItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(255);

            this.Property(t => t.UOM)
                .HasMaxLength(255);

            this.Property(t => t.SKU)
                .HasMaxLength(255);

            this.Property(t => t.Type)
                .HasMaxLength(255);

            this.Property(t => t.PublishedScope)
                .HasMaxLength(255);

            this.Property(t => t.Photo)
                .HasMaxLength(255);

            this.Property(t => t.BodyHtml)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbItem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.UOM).HasColumnName("UOM");
            this.Property(t => t.SKU).HasColumnName("SKU");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.IsAvailable).HasColumnName("IsAvailable");
            this.Property(t => t.CreatedAt).HasColumnName("CreatedAt");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.PublishedAt).HasColumnName("PublishedAt");
            this.Property(t => t.PublishedScope).HasColumnName("PublishedScope");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.BodyHtml).HasColumnName("BodyHtml");
            this.Property(t => t.IsPromoted).HasColumnName("IsPromoted");
            this.Property(t => t.Tags).HasColumnName("Tags");
        }
    }
}
