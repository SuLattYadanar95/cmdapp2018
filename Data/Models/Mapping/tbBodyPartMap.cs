using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbBodyPartMap : EntityTypeConfiguration<tbBodyPart>
    {
        public tbBodyPartMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.BodyPart)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbBodyPart");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.BodyPart).HasColumnName("BodyPart");
            this.Property(t => t.Symptom_English).HasColumnName("Symptom_English");
            this.Property(t => t.Symptom_Myanmar).HasColumnName("Symptom_Myanmar");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Specialty).HasColumnName("Specialty");
            this.Ignore(t => t.Symptom_Myanmar_ZG);
        }
    }
}
