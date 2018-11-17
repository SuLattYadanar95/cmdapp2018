using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Mapping
{
    public class tbSuggestionMap : EntityTypeConfiguration<tbSuggestion>
    {
        public tbSuggestionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.UserId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbSuggestion");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.CreatedAt).HasColumnName("CreatedAt");
            this.Property(t => t.RespondedAt).HasColumnName("RespondedAt");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.IsReponded).HasColumnName("IsReponded");
            this.Property(t => t.ManagedbyId).HasColumnName("ManagedbyId");
            this.Property(t => t.ManagedbyName).HasColumnName("ManagedbyName");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CodeIndex).HasColumnName("CodeIndex");
            this.Property(t => t.OperationType).HasColumnName("OperationType");
            this.Ignore(t => t.MessengerPhone);
        }
    }
}
