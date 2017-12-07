namespace Instagraph.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Instagraph.Models;

    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Caption)
                .IsRequired(true);

            builder.Property(e => e.PictureId)
                .IsRequired(true);

            builder.Property(e => e.UserId)
                .IsRequired(true);
        }
    }
}