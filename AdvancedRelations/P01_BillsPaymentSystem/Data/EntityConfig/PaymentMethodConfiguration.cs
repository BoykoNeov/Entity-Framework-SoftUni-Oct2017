namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            //•	PaymentMethod:
            //o Id -PK
            //o Type – enum (BankAccount, CreditCard)
            //o   UserId
            //o   BankAccountId
            //o   CreditCardId

            // what about null values?
            builder.HasKey(pm => new {pm.UserId, pm.CreditCardId, pm.BankAccountId  });

            builder.Property(b => b.Type)
                .IsRequired(true);

            builder.Property(b => b.UserId)
                .IsRequired(true);
        }
    }
}