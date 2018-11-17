using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbActivityLogMap : EntityTypeConfiguration<tbActivityLog>
    {
        public tbActivityLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.UserId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbActivityLog");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.EventPayload).HasColumnName("EventPayload");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.UserGender).HasColumnName("UserGender");
            this.Property(t => t.UserProPic).HasColumnName("UserProPic");
        }
    }
}
