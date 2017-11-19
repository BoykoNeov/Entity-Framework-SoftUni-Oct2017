namespace P01_BillsPaymentSystem.App
{
    using P01_BillsPaymentSystem.Data.Models;
    using P01_BillsPaymentSystem.Data.Models.Enums;
    using System;
    using System.Globalization;
    using System.Linq;

    internal class UserDetails
    {
        internal static void Get()
        {
            Console.WriteLine("Please enter userId to display information about!");
            int userId = int.Parse(Console.ReadLine());

            var db = new BillsPaymentSystemContext();

            using (db)
            {
                var currentUser = db.Users
                     .Where(u => u.UserId == userId)
                     .Select(u => new
                     {
                        Name = $"{u.FirstName} {u.LastName}",
                         BankAccounts = u.PaymentMethods.Where(x => x.Type == AccountType.BankAccount).Select(pm => pm.BankAccount).ToList(),
                         CreditCards = u.PaymentMethods.Where(x => x.Type == AccountType.CreditCard).Select(pm => pm.CreditCard).ToList(),
                     }).First();

                Console.WriteLine($"User: {currentUser.Name}");
          //      var userCreditCards = currentUser.PaymentMethods.Where(x => x.Type == AccountType.CreditCard);
          //      var userBankAccounts = currentUser.PaymentMethods.Where(x => x.Type == AccountType.BankAccount);

                if (currentUser.BankAccounts.Count() > 0)
                {
                    Console.WriteLine("Bank Accounts:");
                    foreach (BankAccount account in currentUser.BankAccounts)
                    {
                        Console.WriteLine($"-- ID: {account.BankAccountId}");
                        Console.WriteLine($"--- Balance: {account.Balance:f2}");
                        Console.WriteLine($"--- Bank: {account.BankName}");
                        Console.WriteLine($"--- SWIFT: {account.SWIFTCode}");
                    }
                }

                if (currentUser.CreditCards.Count() > 0)
                {
                    Console.WriteLine("Credit Cards:");
                    foreach (CreditCard card in currentUser.CreditCards)
                    {
                        Console.WriteLine($"-- ID: {card.CreditCardId}");
                        Console.WriteLine($"--- Limit: {card.Limit:f2}");
                        Console.WriteLine($"--- Money Owed: {card.MoneyOwed}");
                        Console.WriteLine($"--- Limit Left: {card.LimitLeft}");
                        Console.WriteLine($"Expiration Date {card.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                    }
                }
            }
        }
    }
}