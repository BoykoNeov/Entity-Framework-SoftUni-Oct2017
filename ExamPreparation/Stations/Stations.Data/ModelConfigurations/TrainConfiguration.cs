namespace Stations.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Stations.Models;

    internal class TrainConfiguration : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.TrainNumber);

            builder.Property(e => e.TrainNumber)
                .IsRequired(true)
                .HasMaxLength(10);

            builder.HasMany(e => e.Trips)
                .WithOne(tri => tri.Train)
                .HasForeignKey(e => e.TrainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}