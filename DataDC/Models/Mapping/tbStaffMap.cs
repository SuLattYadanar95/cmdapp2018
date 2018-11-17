using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbStaffMap : EntityTypeConfiguration<tbStaff>
    {
        public tbStaffMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.Username)
                .HasMaxLength(255);

            this.Property(t => t.Password)
                .HasMaxLength(255);

            this.Property(t => t.State)
                .HasMaxLength(255);

            this.Property(t => t.Township)
                .HasMaxLength(255);

            this.Property(t => t.Nationality)
                .HasMaxLength(255);

            this.Property(t => t.Religion)
                .HasMaxLength(255);

            this.Property(t => t.ShopName)
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbStaff");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.Nationality).HasColumnName("Nationality");
            this.Property(t => t.Religion).HasColumnName("Religion");
            this.Property(t => t.Role).HasColumnName("Role");
            this.Property(t => t.Qualification).HasColumnName("Qualification");
            this.Property(t => t.CodeIndex).HasColumnName("CodeIndex");
            this.Property(t => t.ShopId).HasColumnName("ShopId");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.ShopName).HasColumnName("ShopName");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Createdby).HasColumnName("Createdby");
            this.Property(t => t.UniqueID).HasColumnName("UniqueID");
            this.Property(t => t.UserId).HasColumnName("UserId");
        }
    }
}
