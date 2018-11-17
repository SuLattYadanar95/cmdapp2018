using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class StoreMap : EntityTypeConfiguration<Store>
    {
        public StoreMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Active, t.IsDeleted });

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Type)
                .HasMaxLength(255);

            this.Property(t => t.Code)
                .HasMaxLength(255);

            this.Property(t => t.Township)
                .HasMaxLength(255);

            this.Property(t => t.State)
                .HasMaxLength(255);

            this.Property(t => t.Address)
                .HasMaxLength(255);

            this.Property(t => t.Longitude)
                .HasMaxLength(255);

            this.Property(t => t.Latitude)
                .HasMaxLength(255);

            this.Property(t => t.Baseboardserial)
                .HasMaxLength(255);

            this.Property(t => t.AgencyPhno)
                .HasMaxLength(255);

            this.Property(t => t.Agencympin)
                .HasMaxLength(255);

            this.Property(t => t.AgencyCode)
                .HasMaxLength(255);

            this.Property(t => t.AreaManagerName)
                .HasMaxLength(255);

            this.Property(t => t.CreatedbyName)
                .HasMaxLength(255);

            this.Property(t => t.Contact)
                .HasMaxLength(255);

            this.Property(t => t.Email)
                .HasMaxLength(255);

            this.Property(t => t.Machineguid)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Store");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Createdby).HasColumnName("Createdby");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.CodeIndex).HasColumnName("CodeIndex");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Baseboardserial).HasColumnName("Baseboardserial");
            this.Property(t => t.AgencyPhno).HasColumnName("AgencyPhno");
            this.Property(t => t.Agencympin).HasColumnName("Agencympin");
            this.Property(t => t.AgencyCode).HasColumnName("AgencyCode");
            this.Property(t => t.AreaManagerId).HasColumnName("AreaManagerId");
            this.Property(t => t.AreaManagerName).HasColumnName("AreaManagerName");
            this.Property(t => t.CreatedbyName).HasColumnName("CreatedbyName");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Machineguid).HasColumnName("Machineguid");
        }
    }
}
