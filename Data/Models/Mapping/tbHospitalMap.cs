using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbHospitalMap : EntityTypeConfiguration<tbHospital>
    {
        public tbHospitalMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Phone)
                .HasMaxLength(255);

            this.Property(t => t.Address)
                .HasMaxLength(255);

            this.Property(t => t.Township)
                .HasMaxLength(255);

            this.Property(t => t.State)
                .HasMaxLength(255);

            this.Property(t => t.Type)
                .HasMaxLength(255);

            this.Property(t => t.Image)
                .HasMaxLength(50);

            this.Property(t => t.Website)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("tbHospital");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Specialty).HasColumnName("Specialty");
            this.Property(t => t.SpecialtyMyan).HasColumnName("SpecialtyMyan");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.WelcomePhoto).HasColumnName("WelcomePhoto");
            this.Ignore(t => t.WelcomePhotoUrl);
            this.Ignore(t => t.PhotoUrl);
            this.Ignore(t => t.Name_ZG);
            this.Ignore(t => t.Address_ZG);
            this.Ignore(t => t.Township_ZG);
            this.Ignore(t => t.State_ZG);
        }
    }
}
