namespace P01_BillsPaymentSystem.App
{
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
    using P01_BillsPaymentSystem.Data.Models.Enums;
    using System;
    using System.Linq;

    internal class Bills
    {
        public static void PayBills(int userId, decimal amountToPay)
        {
            var dbContext = new BillsPaymentSystemContext();
            using (dbContext)
            {
                PaymentMethod[] accountsMethods = dbContext.Users
                    .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.BankAccount)
                    .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.CreditCard)
                    .FirstOrDefault(u => u.UserId == userId)
                    .PaymentMethods
                    .Where(pm => pm.Type == AccountType.BankAccount)
                    .OrderBy(pm => pm.BankAccountId)
                    .ToArray();

                PaymentMethod[] cardsMethods = dbContext.Users
                    .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.BankAccount)
                    .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.CreditCard)
                    .FirstOrDefault(u => u.UserId == userId)
                    .PaymentMethods
                    .Where(pm => pm.Type == AccountType.CreditCard)
                    .OrderBy(pm => pm.CreditCardId)
                    .ToArray();

                decimal moneyInCards = cardsMethods.Sum(c => c.CreditCard.LimitLeft);
                decimal moneyInAccounts = accountsMethods.Sum(pm => pm.BankAccount.Balance);
                decimal totalMoney = moneyInAccounts + moneyInCards;

                if (totalMoney < amountToPay)
                {
                    Console.WriteLine("Insufficient money");
                    return;
                }
                else
                {
                    foreach (PaymentMethod account in accountsMethods)
                    {
                        if (amountToPay == 0 || moneyInAccounts == 0)
                        {
                            break;
                        }

                        decimal amountToWithdraw = Math.Min(account.BankAccount.Balance, amountToPay);
                        account.BankAccount.Balance -= amountToWithdraw;
                        moneyInAccounts -= amountToWithdraw;
                        amountToPay -= amountToWithdraw;
                    }

                    foreach (PaymentMethod card in cardsMethods)
                    {
                        if (amountToPay == 0 || moneyInCards == 0)
                        {
                            break;
                        }

                        decimal amountToWithdraw = Math.Min(card.CreditCard.LimitLeft, amountToPay);
                        card.CreditCard.MoneyOwed += amountToWithdraw;
                        moneyInCards -= amountToWithdraw;
                        amountToPay -= amountToWithdraw;
                    }

                    dbContext.SaveChanges();
                    Console.WriteLine("Bills are paid!");
                }
            }
        }
    }
}