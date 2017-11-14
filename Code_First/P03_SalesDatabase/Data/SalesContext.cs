namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using P03_SalesDatabase.Data.Models;
    using P03_SalesDatabase;

    public class SalesContext : DbContext
    {
        public SalesContext() { }

        public SalesContext(DbContextOptions options) : base()
        { }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(e => e.Quantity)
                .IsRequired(true);

                entity.Property(e => e.Price)
                .IsRequired(true);

                entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(100);

                entity.Property(e => e.Email)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(80);

                entity.Property(e => e.CreditCardNumber)
                .IsRequired(true);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(80);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId);

                entity.Property(e => e.Date)
                .IsRequired(true)
                .HasColumnName("DATETIME2")
                // this was already done initially
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(e => e.ProductId)
                .HasConstraintName("FK_Sales_Product");

                entity.HasOne(e => e.Customer)
                .WithMany(p => p.Sales)
                .HasForeignKey(e => e.CustomerId);

                entity.HasOne(e => e.Store)
                .WithMany(p => p.Sales)
                .HasForeignKey(e => e.StoreId);

            });

        }
    }
}