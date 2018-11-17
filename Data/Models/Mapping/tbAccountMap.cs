using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbAccountMap : EntityTypeConfiguration<tbAccount>
    {
        public tbAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Username)
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            this.Property(t => t.Role)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbAccount");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Role).HasColumnName("Role");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
        }
    }
}
