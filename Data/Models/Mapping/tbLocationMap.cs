using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbLocationMap : EntityTypeConfiguration<tbLocation>
    {
        public tbLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DialingCode)
                .HasMaxLength(255);

            this.Property(t => t.Country)
                .HasMaxLength(255);

            this.Property(t => t.StateDivision)
                .HasMaxLength(255);

            this.Property(t => t.DivisionCode)
                .HasMaxLength(255);

            this.Property(t => t.Township)
                .HasMaxLength(255);

            this.Property(t => t.TownshipCode)
                .HasMaxLength(255);

            this.Property(t => t.Createdby)
                .HasMaxLength(255);

            this.Property(t => t.Accesstime)
                .HasMaxLength(255);

            this.Property(t => t.Abbreviation)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbLocation");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DialingCode).HasColumnName("DialingCode");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.StateDivision).HasColumnName("StateDivision");
            this.Property(t => t.DivisionCode).HasColumnName("DivisionCode");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.TownshipCode).HasColumnName("TownshipCode");
            this.Property(t => t.Createdby).HasColumnName("Createdby");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Abbreviation).HasColumnName("Abbreviation");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
