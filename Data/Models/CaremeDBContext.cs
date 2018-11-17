using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Data.Models.Mapping;

namespace Data.Models
{
    public partial class CaremeDBContext : DbContext
    {
        static CaremeDBContext()
        {
            Database.SetInitializer<CaremeDBContext>(null);
        }

        public CaremeDBContext()
            : base("Name=CaremeDBContext")
        {
        }

        public DbSet<tbAccount> tbAccounts { get; set; }
        public DbSet<tbActivityLog> tbActivityLogs { get; set; }
        public DbSet<tbAppointment> tbAppointments { get; set; }
        public DbSet<tbBodyPart> tbBodyParts { get; set; }
        public DbSet<tbDoctor> tbDoctors { get; set; }
        public DbSet<tbDoctorHospital> tbDoctorHospitals { get; set; }
        public DbSet<tbHospital> tbHospitals { get; set; }
        public DbSet<tbLocation> tbLocations { get; set; }
        public DbSet<tbLogin> tbLogins { get; set; }
        public DbSet<tbPatient> tbPatients { get; set; }
        public DbSet<tbPaymentCode> tbPaymentCodes { get; set; }
        public DbSet<tbSpecialty> tbSpecialties { get; set; }
        public DbSet<tbStaff> tbStaffs { get; set; }
        public DbSet<tbSchedule> tbSchedules { get; set; }
        public DbSet<tbDomain> tbDomains { get; set; }
        public DbSet<tbProcedure> tbProcedures { get; set; }
        public DbSet<tbService> tbServices { get; set; }
        public DbSet<tbFAQ> tbFAQs { get; set; }
        public DbSet<tbSuggestion> tbSuggestions { get; set; }

        public DbSet<tbScheduleData> tbScheduleDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new tbAccountMap());
            modelBuilder.Configurations.Add(new tbActivityLogMap());
            modelBuilder.Configurations.Add(new tbAppointmentMap());
            modelBuilder.Configurations.Add(new tbBodyPartMap());
            modelBuilder.Configurations.Add(new tbDoctorMap());
            modelBuilder.Configurations.Add(new tbDoctorHospitalMap());
            modelBuilder.Configurations.Add(new tbHospitalMap());
            modelBuilder.Configurations.Add(new tbLocationMap());
            modelBuilder.Configurations.Add(new tbLoginMap());
            modelBuilder.Configurations.Add(new tbPatientMap());
            modelBuilder.Configurations.Add(new tbPaymentCodeMap());
            modelBuilder.Configurations.Add(new tbSpecialtyMap());
            modelBuilder.Configurations.Add(new tbStaffMap());
            modelBuilder.Configurations.Add(new tbScheduleMap());
            modelBuilder.Configurations.Add(new tbDomainMap());
            modelBuilder.Configurations.Add(new tbProcedureMap());
            modelBuilder.Configurations.Add(new tbServiceMap());
            modelBuilder.Configurations.Add(new tbFAQMap());
            modelBuilder.Configurations.Add(new tbSuggestionMap());
            modelBuilder.Configurations.Add(new tbScheduleDataMap());
        }
    }
}
