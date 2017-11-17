namespace P01_BillsPaymentSystem.Data.Models
{
    using System;

    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public decimal Limit
        {
            get
            {
                return this.Limit;
            }
            set
            {
                if (value >= 0)
                {
                    this.Limit = value;
                }
                else
                {
                    throw new ArgumentException("Limit cannot be negative");
                }
            }
        }
        public decimal MoneyOwed
        {
            get
            {
                return this.MoneyOwed;
            }
            set
            {
                this.MoneyOwed = value;
            }
        }
        public decimal LimitLeft
        {
            get
            {
                return (this.Limit - this.LimitLeft);
            }
        }

        public DateTime ExpirationDate { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}