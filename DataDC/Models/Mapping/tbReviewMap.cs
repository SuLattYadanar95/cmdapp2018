using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbReviewMap : EntityTypeConfiguration<tbReview>
    {
        public tbReviewMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.MemberId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbReview");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Detail).HasColumnName("Detail");
            this.Property(t => t.ShopId).HasColumnName("ShopId");
            this.Property(t => t.MemberId).HasColumnName("MemberId");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsReported).HasColumnName("IsReported");
        }
    }
}
