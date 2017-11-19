namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");

            //•	PaymentMethod:
            //o Id -PK
            //o Type – enum (BankAccount, CreditCard)
            //o   UserId
            //o   BankAccountId
            //o   CreditCardId

            // what about null values?
            // builder.HasKey(pm => new {pm.UserId, pm.CreditCardId, pm.BankAccountId  });

            builder.HasIndex(i => new {i.UserId, i.CreditCardId, i.BankAccountId })
                .IsUnique(true);

            builder.HasKey(b => b.Id);

            builder.HasOne(e => e.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.BankAccount)
                .WithOne(b => b.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.BankAccountId)
                .IsRequired(false);

            builder.HasOne(e => e.CreditCard)
                .WithOne(c => c.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.CreditCardId)
                .IsRequired(false);

            builder.Property(b => b.Type)
                .IsRequired(true);
        }
    }
}