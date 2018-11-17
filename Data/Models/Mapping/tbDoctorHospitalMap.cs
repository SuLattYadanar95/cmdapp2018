using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbDoctorHospitalMap : EntityTypeConfiguration<tbDoctorHospital>
    {
        public tbDoctorHospitalMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DoctorName)
                .HasMaxLength(255);

            this.Property(t => t.HospitalName)
                .HasMaxLength(255);

            this.Property(t => t.AvailableTime)
                .HasMaxLength(255);

            this.Property(t => t.AvailableDay)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("tbDoctorHospital");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DoctorID).HasColumnName("DoctorID");
            this.Property(t => t.DoctorName).HasColumnName("DoctorName");
            this.Property(t => t.HospitalID).HasColumnName("HospitalID");
            this.Property(t => t.HospitalName).HasColumnName("HospitalName");
            this.Property(t => t.AvailableTime).HasColumnName("AvailableTime");
            this.Property(t => t.AvailableDay).HasColumnName("AvailableDay");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
        }
    }
}
