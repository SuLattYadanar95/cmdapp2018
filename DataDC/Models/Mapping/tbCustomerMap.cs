using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbCustomerMap : EntityTypeConfiguration<tbCustomer>
    {
        public tbCustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Note)
                .HasMaxLength(50);

            this.Property(t => t.State)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbCustomer");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.AddressId).HasColumnName("AddressId");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.CreatedAt).HasColumnName("CreatedAt");
            this.Property(t => t.AcceptMarketing).HasColumnName("AcceptMarketing");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
