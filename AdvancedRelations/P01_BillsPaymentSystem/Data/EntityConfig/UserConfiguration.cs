namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        //o UserId
        //o FirstName(up to 50 characters, unicode)
        //o LastName(up to 50 characters, unicode)
        //o Email(up to 80 characters, non-unicode)
        //o Password(up to 25 characters, non-unicode)
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(b => b.UserId);

            builder.Property(b => b.FirstName)
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(b => b.LastName)
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(b => b.Email)
                .IsUnicode(false)
                .IsRequired(true)
                .HasMaxLength(80);

            builder.Property(b => b.Password)
                .IsUnicode(false)
                .IsRequired(true)
                .HasMaxLength(25);
        }
    }
}