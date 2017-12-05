namespace ProductsShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProductsShop.Models;

    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired(true);

            builder.Property(e => e.Price)
                .IsRequired(true);

            builder.Property(e => e.BuyerId)
                .IsRequired(false);

            builder.Property(e => e.SellerId)
                .IsRequired(true);
        }
    }
}