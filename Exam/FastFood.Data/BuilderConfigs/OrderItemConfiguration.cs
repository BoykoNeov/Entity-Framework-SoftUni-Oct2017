namespace FastFood.Data
{
    using FastFood.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => new { e.OrderId, e.ItemId});

            builder.HasOne(e => e.Order)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(e => e.OrderId);

            builder.HasOne(e => e.Item)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(e => e.ItemId);

        }
    }
}