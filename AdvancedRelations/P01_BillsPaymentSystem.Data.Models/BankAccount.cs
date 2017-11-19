namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public string SWIFTCode { get; set; }

        //Okay, this shouln't be nullable, but how we can create the two things at the same time - we try to create the first PaymentMethod, which must have at least one
        // one of the two not null - either a bank acc or a credit card. But haven't got any, to they both are null, which is forbidden. At the same time
        // if the PaymentMethodId in BankAccount or CreditCard can't be null, we can't create one, because we don't have any payment method.
        // If it wont exist in the DB, then ok, it can be not nullable
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Withdraws money from the bank account
        /// </summary>
        /// <param name="amount">Amount of money to be withdrawn</param>
        /// <returns>returns if the operation was successfull</returns>
        public bool Withdraw(decimal amount)
        {
            if (this.Balance >= amount)
            {
                this.Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }
    }
}