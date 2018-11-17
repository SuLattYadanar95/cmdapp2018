using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Mapping
{
    public class tbScheduleDataMap : EntityTypeConfiguration<tbScheduleData>
    {
        public tbScheduleDataMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            //this.Property(t => t.ID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("tbScheduleData");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AppointmentDatetime).HasColumnName("AppointmentDatetime");
            this.Property(t => t.AppointmentTime).HasColumnName("AppointmentTime");
            this.Property(t => t.DoctorID).HasColumnName("DoctorID");
            this.Property(t => t.DoctorName).HasColumnName("DoctorName");
            this.Property(t => t.HospitalID).HasColumnName("HospitalID");
            this.Property(t => t.HospitalName).HasColumnName("HospitalName");
            this.Property(t => t.Day).HasColumnName("Day");
            this.Property(t => t.MaxPatientCount).HasColumnName("MaxPatientCount");
            this.Property(t => t.Fromtime).HasColumnName("Fromtime");
            this.Property(t => t.Totime).HasColumnName("Totime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsCancelled).HasColumnName("IsCancelled");
            this.Property(t => t.ReachedPatientCount).HasColumnName("ReachedPatientCount");
            this.Property(t => t.ScheduleID).HasColumnName("ScheduleID");

            this.Property(t => t.IsStopped).HasColumnName("IsStopped");
            this.Property(t => t.IsLimited).HasColumnName("IsLimited");
            this.Property(t => t.DefaultPatientCount).HasColumnName("DefaultPatientCount");

        }
    }
}


