using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbSpecialtyMap : EntityTypeConfiguration<tbSpecialty>
    {
        public tbSpecialtyMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Specialty)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbSpecialty");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Specialty).HasColumnName("Specialty");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Symptom_English).HasColumnName("Symptom_English");
            this.Property(t => t.Symptom_Myanmar).HasColumnName("Symptom_Myanmar");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");

            
            this.Ignore(t => t.Specialty_ZG);
            this.Ignore(t => t.Symptom_Myanmar_ZG);
        }
    }
}
