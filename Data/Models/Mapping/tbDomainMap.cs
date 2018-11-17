using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbDomainMap : EntityTypeConfiguration<tbDomain>
    {
        public tbDomainMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

        
            // Table & Column Mappings
            this.ToTable("tbDomain");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.BodyHtml).HasColumnName("BodyHtml");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Tags).HasColumnName("Tags");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.HospitalId).HasColumnName("HospitalId");
        }
    }
}
