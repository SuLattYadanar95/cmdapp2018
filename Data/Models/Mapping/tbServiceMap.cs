using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbServiceMap : EntityTypeConfiguration<tbService>
    {
        public tbServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
          
            // Table & Column Mappings
            this.ToTable("tbService");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BodyHtml).HasColumnName("BodyHtml");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsPublished).HasColumnName("IsPublished");
            this.Property(t => t.DomainId).HasColumnName("DomainId");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.HospitalId).HasColumnName("HospitalId");
            this.Property(t => t.Phone).HasColumnName("Phone");
        }
    }
}
