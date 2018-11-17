using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbRateMap : EntityTypeConfiguration<tbRate>
    {
        public tbRateMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.MemberID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbRate");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.ShopID).HasColumnName("ShopID");
            this.Property(t => t.MemberID).HasColumnName("MemberID");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
        }
    }
}
