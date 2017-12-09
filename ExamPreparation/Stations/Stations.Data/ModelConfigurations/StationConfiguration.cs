namespace Stations.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Stations.Models;

    internal class StationConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Name);

            builder.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(e => e.Town)
                .IsRequired(true)
                .HasMaxLength(50);
        }
    }
}