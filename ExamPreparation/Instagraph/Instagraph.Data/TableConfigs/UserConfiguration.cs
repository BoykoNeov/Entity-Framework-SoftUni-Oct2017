namespace Instagraph.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Instagraph.Models;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
       public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(e => e.Username)
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(e => e.Password)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(e => e.ProfilePictureId)
                .IsRequired(true);

            builder.HasOne(u => u.ProfilePicture)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}