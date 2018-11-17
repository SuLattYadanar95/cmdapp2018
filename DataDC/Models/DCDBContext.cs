using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataDC.Models.Mapping;

namespace DataDC.Models
{
    public partial class DCDBContext : DbContext
    {
        static DCDBContext()
        {
            Database.SetInitializer<DCDBContext>(null);
        }

        public DCDBContext()
            : base("Name=DCDBContext")
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<tbActivityLog> tbActivityLogs { get; set; }
        public DbSet<tbAddress> tbAddresses { get; set; }
        public DbSet<tbCategory> tbCategories { get; set; }
        public DbSet<tbCustomer> tbCustomers { get; set; }
        public DbSet<tbImage> tbImages { get; set; }
        public DbSet<tbItem> tbItems { get; set; }
        public DbSet<tbOrder> tbOrders { get; set; }
        public DbSet<tbOrderDetail> tbOrderDetails { get; set; }
        public DbSet<tbPhoto> tbPhotoes { get; set; }
        public DbSet<tbRate> tbRates { get; set; }
        public DbSet<tbReview> tbReviews { get; set; }
        public DbSet<tbShop> tbShops { get; set; }
        public DbSet<tbShopCategory> tbShopCategories { get; set; }
        public DbSet<tbShopCategoryItem> tbShopCategoryItems { get; set; }
        public DbSet<tbStaff> tbStaffs { get; set; }
        public DbSet<tbSubcategory> tbSubcategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StoreMap());
            modelBuilder.Configurations.Add(new tbActivityLogMap());
            modelBuilder.Configurations.Add(new tbAddressMap());
            modelBuilder.Configurations.Add(new tbCategoryMap());
            modelBuilder.Configurations.Add(new tbCustomerMap());
            modelBuilder.Configurations.Add(new tbImageMap());
            modelBuilder.Configurations.Add(new tbItemMap());
            modelBuilder.Configurations.Add(new tbOrderMap());
            modelBuilder.Configurations.Add(new tbOrderDetailMap());
            modelBuilder.Configurations.Add(new tbPhotoMap());
            modelBuilder.Configurations.Add(new tbRateMap());
            modelBuilder.Configurations.Add(new tbReviewMap());
            modelBuilder.Configurations.Add(new tbShopMap());
            modelBuilder.Configurations.Add(new tbShopCategoryMap());
            modelBuilder.Configurations.Add(new tbShopCategoryItemMap());
            modelBuilder.Configurations.Add(new tbStaffMap());
            modelBuilder.Configurations.Add(new tbSubcategoryMap());
        }
    }
}
