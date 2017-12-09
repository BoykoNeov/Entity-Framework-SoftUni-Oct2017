namespace Stations.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Stations.Models;

    internal class TrainSeatConfiguration : IEntityTypeConfiguration<TrainSeat>
    {
        public void Configure(EntityTypeBuilder<TrainSeat> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TrainId)
                .IsRequired(true);

            builder.Property(e => e.SeatingClassId)
                .IsRequired(true);

            builder.Property(e => e.Quantity)
                .IsRequired(true);

            //builder.HasOne(e => e.SeatingClass)
            //    .WithMany(sc => sc.TrainSeats)
            //    .HasForeignKey(e => e.SeatingClassId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SeatingClass)
                .WithMany();

            builder.HasOne(e => e.Train)
                .WithMany(t => t.TrainSeats)
                .HasForeignKey(e => e.TrainId);
        }
    }
}