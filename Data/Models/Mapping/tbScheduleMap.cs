using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbScheduleMap : EntityTypeConfiguration<tbSchedule>
    {
        public tbScheduleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            //this.Property(t => t.ID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("tbSchedule");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.HospitalID).HasColumnName("HospitalID");
            this.Property(t => t.HospitalName).HasColumnName("HospitalName");
            this.Property(t => t.Fromtime).HasColumnName("Fromtime");
            this.Property(t => t.Totime).HasColumnName("Totime");
            this.Property(t => t.DoctorID).HasColumnName("DoctorID");
            this.Property(t => t.DoctorName).HasColumnName("DoctorName");
            this.Property(t => t.IsSunday).HasColumnName("IsSunday");
            this.Property(t => t.IsMonday).HasColumnName("IsMonday");
            this.Property(t => t.IsTuesday).HasColumnName("IsTuesday");
            this.Property(t => t.IsWednesday).HasColumnName("IsWednesday");
            this.Property(t => t.IsThursday).HasColumnName("IsThursday");
            this.Property(t => t.IsFriday).HasColumnName("IsFriday");
            this.Property(t => t.IsSaturday).HasColumnName("IsSaturday");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.PatientLimit).HasColumnName("PatientLimit");

            this.Ignore(t => t.HospitalName_ZG);
            this.Ignore(t => t.DoctorName_ZG);
        }
    }
}
