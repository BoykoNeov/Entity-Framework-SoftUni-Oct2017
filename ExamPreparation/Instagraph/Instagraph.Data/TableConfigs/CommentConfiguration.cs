namespace Instagraph.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Instagraph.Models;

    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Content)
                .HasMaxLength(250)
                .IsRequired(true);

            builder.Property(e => e.PostId)
                .IsRequired(true);

            builder.Property(e => e.UserId)
                .IsRequired(true);
        }
    }
}