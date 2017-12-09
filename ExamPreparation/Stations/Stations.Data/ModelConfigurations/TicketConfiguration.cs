namespace Stations.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Stations.Models;

    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Price)
                .IsRequired(true);

            //builder.HasOne(t => t.Trip)
            //    .WithMany(tr => tr.Tickets)
            //    .HasForeignKey(t => t.TripId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Trip)
                .WithMany();

            builder.HasOne(t => t.CustomerCard)
                .WithMany(cc => cc.BoughtTickets)
                .HasForeignKey(t => t.CustomerCardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}