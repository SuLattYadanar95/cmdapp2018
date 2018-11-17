using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbProcedureMap : EntityTypeConfiguration<tbProcedure>
    {
        public tbProcedureMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbProcedure");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ServiceID).HasColumnName("ServiceID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.StepIndex).HasColumnName("StepIndex");
            this.Property(t => t.IsMandatory).HasColumnName("IsMandatory");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.ServiceTitle).HasColumnName("ServiceTitle");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.BodyHTML).HasColumnName("BodyHtml");
            this.Property(t => t.AttachmentUrls).HasColumnName("AttachmentUrls");
        }
    }
}
