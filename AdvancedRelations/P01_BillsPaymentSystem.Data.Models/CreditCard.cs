namespace P01_BillsPaymentSystem.Data.Models
{
    using System;

    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public decimal Limit { get; set; }
        public decimal MoneyOwed { get; set; }
        public decimal LimitLeft
        {
            get
            {
                return (this.Limit - this.MoneyOwed);
            }
        }

        public DateTime ExpirationDate { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Withdraws money from the credit card
        /// </summary>
        /// <param name="amount">Amount of money to be withdrawn</param>
        /// <returns>returns if the operation is successfull</returns>
        public bool Withdraw(decimal amount)
        {
            if (this.LimitLeft >= amount)
            {
                this.MoneyOwed += amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Deposit(decimal amount)
        {

        }
    }
}