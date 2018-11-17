using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class tbFAQMap : EntityTypeConfiguration<tbFAQ>
    {
        public tbFAQMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
         
            // Table & Column Mappings
            this.ToTable("tbFAQ");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Question).HasColumnName("Question");
            this.Property(t => t.Answer).HasColumnName("Answer");
            this.Property(t => t.AnswerHTML).HasColumnName("AnswerHTML");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.IsPublished).HasColumnName("IsPublished");
         
        }
    }
}
