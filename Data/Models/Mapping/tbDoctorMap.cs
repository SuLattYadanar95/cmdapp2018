using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbDoctorMap : EntityTypeConfiguration<tbDoctor>
    {
        public tbDoctorMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Specialty)
                .HasMaxLength(255);

            this.Property(t => t.Qualification)
                .HasMaxLength(255);

           
            this.Property(t => t.Type)
                .HasMaxLength(255);

            this.Property(t => t.Pin)
               .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbDoctor");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
           
            this.Property(t => t.Specialty).HasColumnName("Specialty");
            this.Property(t => t.SpecialtyMyan).HasColumnName("SpecialtyMyan");
            this.Property(t => t.Qualification).HasColumnName("Qualification");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.LicenseID).HasColumnName("LicenseID");
            this.Property(t => t.IsSpecialist).HasColumnName("IsSpecialist");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Pin).HasColumnName("Pin");
            this.Property(t => t.SpecialityID).HasColumnName("SpecialityID");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.UserToken).HasColumnName("UserToken");

            this.Ignore(t => t.Image);
            this.Ignore(t => t.PhotoUrl);
            this.Ignore(t => t.SystemStatus);
            this.Ignore(t => t.Name_ZG);
            this.Ignore(t => t.SpecialtyMyan_ZG);

           
            
        }
    }
}
