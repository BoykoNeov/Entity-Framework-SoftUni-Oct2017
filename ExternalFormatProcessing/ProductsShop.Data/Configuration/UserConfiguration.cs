namespace ProductsShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProductsShop.Models;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .IsRequired(false);

            builder.Property(e => e.LastName)
                .IsRequired(true);

            builder.Property(e => e.Age)
                .IsRequired(false);

            builder.HasMany(e => e.ProductsBought)
                .WithOne(p => p.Buyer)
                .HasForeignKey(p => p.BuyerId);

            builder.HasMany(e => e.ProductsSold)
                .WithOne(p => p.Seller)
                .HasForeignKey(e => e.SellerId);
        }
    }
}