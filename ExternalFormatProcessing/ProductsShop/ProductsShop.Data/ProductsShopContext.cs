namespace ProductsShop.Data
{
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Models;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected ProductsShopContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
    }
}
