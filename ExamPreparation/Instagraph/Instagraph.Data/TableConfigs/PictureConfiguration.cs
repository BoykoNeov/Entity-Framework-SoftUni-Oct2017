namespace Instagraph.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Instagraph.Models;

    internal class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
      public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Path)
                .IsRequired(true);

            builder.Property(e => e.Size)
                .IsRequired(true);
        }
    }
}