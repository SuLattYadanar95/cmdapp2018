using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDC.Models.Mapping
{
    public class tbAddressMap : EntityTypeConfiguration<tbAddress>
    {
        public tbAddressMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CustomerId)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Latitude)
                .HasMaxLength(50);

            this.Property(t => t.Longitude)
                .HasMaxLength(50);

            this.Property(t => t.Zip)
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .HasMaxLength(50);

            this.Property(t => t.CountryCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbAddress");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Zip).HasColumnName("Zip");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Accesstime).HasColumnName("Accesstime");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.Geolocation).HasColumnName("Geolocation");
        }
    }
}
