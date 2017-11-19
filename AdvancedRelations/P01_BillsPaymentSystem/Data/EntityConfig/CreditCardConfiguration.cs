namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    internal class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        // CreditCard:
        //o CreditCardId
        //o Limit
        //o MoneyOwed
        //o LimitLeft(calculated property, not included in the database)
        //o ExpirationDate
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("CreditCards");

            builder.HasKey(b => b.CreditCardId);

            // check if this is correct
            //builder.HasOne(b => b.PaymentMethod)
            //    .WithOne(b => b.CreditCard)
            //    .HasForeignKey<PaymentMethod>(pm => pm.BankAccountId)
            //    .IsRequired(false);

            builder.Ignore(b => b.PaymentMethodId);

            builder.Property(b => b.ExpirationDate)
                .IsRequired(true)
                .HasColumnType("DATETIME2");

            builder.Property(b => b.MoneyOwed)
                .IsRequired(true);

            builder.Property(b => b.Limit)
                .IsRequired(true);

            builder.Ignore(b => b.LimitLeft);
        }
    }
}