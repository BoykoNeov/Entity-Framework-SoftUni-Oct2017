namespace Stations.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Stations.Models;

    internal class SeatingClassConfiguration : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(sc => new { sc.Name, sc.Abbreviation });

            builder.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(30);
        }
    }
}