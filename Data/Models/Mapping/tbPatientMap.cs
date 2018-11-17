using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbPatientMap : EntityTypeConfiguration<tbPatient>
    {
        public tbPatientMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            

            // Table & Column Mappings
            this.ToTable("tbPatient");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.Problem).HasColumnName("Problem");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UserType).HasColumnName("UserType");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.MsgrID).HasColumnName("MsgrID");
            this.Property(t => t.MsgrName).HasColumnName("MsgrName");
            

            this.Ignore(t => t.Name_ZG);
    }
    }
}
