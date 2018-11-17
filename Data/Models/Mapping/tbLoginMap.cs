using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbLoginMap : EntityTypeConfiguration<tbLogin>
    {
        public tbLoginMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Role)
                .HasMaxLength(50);

            this.Property(t => t.ShopName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbLogin");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.LoginTime).HasColumnName("LoginTime");
            this.Property(t => t.LogoutTime).HasColumnName("LogoutTime");
            this.Property(t => t.Role).HasColumnName("Role");
            this.Property(t => t.Createdby).HasColumnName("Createdby");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ShopName).HasColumnName("ShopName");
            this.Property(t => t.ShopId).HasColumnName("ShopId");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
        }
    }
}
