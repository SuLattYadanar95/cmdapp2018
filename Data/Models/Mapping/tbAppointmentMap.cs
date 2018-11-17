using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Mapping
{
    class tbAppointmentMap : EntityTypeConfiguration<tbAppointment>
    {
        public tbAppointmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

         
            // Table & Column Mappings
            this.ToTable("tbAppointment");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.PatientId).HasColumnName("PatientId");
            this.Property(t => t.PatientName).HasColumnName("PatientName");
            this.Property(t => t.PatientAge).HasColumnName("PatientAge");
            this.Property(t => t.DoctorId).HasColumnName("DoctorId");
            this.Property(t => t.DoctorName).HasColumnName("DoctorName");
            this.Property(t => t.HospitalId).HasColumnName("HospitalId");
            this.Property(t => t.HospitalName).HasColumnName("HospitalName");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AppointmentDateTime).HasColumnName("AppointmentDateTime");
            this.Property(t => t.Counter).HasColumnName("Counter");
            this.Property(t => t.IsWaiting).HasColumnName("IsWaiting");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ScheduleID).HasColumnName("ScheduleID");
            this.Property(t => t.Day).HasColumnName("Day");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsEmergency).HasColumnName("IsEmergency");
            this.Property(t => t.IsEmergencyApproved).HasColumnName("IsEmergencyApproved");

            this.Property(t => t.Problem).HasColumnName("Problem");
            this.Property(t => t.BookingType).HasColumnName("BookingType");
            this.Property(t => t.IsCheckIn).HasColumnName("IsCheckIn");
            this.Property(t => t.IsDelByAdmin).HasColumnName("IsDelByAdmin");
            this.Property(t => t.ScheduleDataID).HasColumnName("ScheduleDataID");
            this.Property(t => t.IsApproved).HasColumnName("IsApproved");

            this.Property(t => t.SkipCount).HasColumnName("SkipCount");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.CreatedTime).HasColumnName("CreatedTime");

            this.Ignore(t => t.Gender);
            this.Ignore(t => t.PatientName_ZG);
            this.Ignore(t => t.DoctorName_ZG);
            this.Ignore(t => t.HospitalName_ZG);
            this.Ignore(t => t.Problem_ZG);
        }
    }
}
