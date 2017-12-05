namespace ProductsShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProductsShop.Models;

    public class CategoryProductsConfiguration : IEntityTypeConfiguration<CategoryProducts>
    {
        public void Configure(EntityTypeBuilder<CategoryProducts> builder)
        {
            builder.HasKey(e => new {e.ProductId, e.CateogryId });

            builder.HasOne(e => e.Category)
                .WithMany(b => b.ThisCategoryProducts)
                .HasForeignKey(e => e.CateogryId);

            builder.HasOne(e => e.Product)
                .WithMany(p => p.ThisProductsCategories)
                .HasForeignKey(e => e.ProductId);
        }
    }
}