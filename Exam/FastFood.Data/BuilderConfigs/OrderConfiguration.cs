namespace FastFood.Data
{
    using FastFood.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Customer)
                .IsRequired(true);

            builder.Property(e => e.DateTime)
                .IsRequired(true);

            builder.Ignore(e => e.TotalPrice);


        }
    }
}