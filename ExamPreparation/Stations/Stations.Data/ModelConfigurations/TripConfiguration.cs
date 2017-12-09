using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
namespace Stations.Data
{
    internal class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.OriginStationId)
                .IsRequired(true);

            builder.Property(e => e.DestinationStationId)
                .IsRequired(true);

            builder.Property(e => e.TrainId)
                .IsRequired(true);

            builder.Property(e => e.DepartureTime)
                .IsRequired(true);

            builder.Property(e => e.ArrivalTime)
                .IsRequired(true);

            builder.HasOne(e => e.DestinationStation)
                .WithMany(ds => ds.TripsTo)
                .HasForeignKey(e => e.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.OriginStation)
                .WithMany(os => os.TripsFrom)
                .HasForeignKey(e => e.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}