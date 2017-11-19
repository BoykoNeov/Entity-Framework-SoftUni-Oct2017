namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    internal class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        //o BankAccountId
        //o Balance
        //o BankName(up to 50 characters, unicode)
        //o SWIFT Code(up to 20 characters, non-unicode)
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccounts");

            builder.HasKey(b => b.BankAccountId);

            // check if this is correct
            //builder.HasOne(e => e.PaymentMethod)
            //    .WithOne(e => e.BankAccount)
            //    .HasForeignKey<PaymentMethod>(pm => pm.BankAccountId)
            //    .IsRequired(false);

            builder.Ignore(b => b.PaymentMethodId);

            builder.Property(b => b.BankName)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(b => b.SWIFTCode)
                .IsRequired(true)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(b => b.Balance)
                .IsRequired(true);


        }
    }
}