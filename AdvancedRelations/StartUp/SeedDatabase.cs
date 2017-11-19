namespace P01_BillsPaymentSystem.App
{
    using P01_BillsPaymentSystem.Data.Models;
    using P01_BillsPaymentSystem.Data.Models.Enums;
    using System;
    using System.Globalization;

    class SeedDatabase
    {
        internal static void SeedDb(BillsPaymentSystemContext currentDb)
        {
            using (currentDb)
            {
                User[] users = new User[] {
                    new User()
                    {
                        FirstName = "Ivanov",
                        LastName = "Petrov",
                        Email = "Ipetrov@abv.bg",
                        Password = "Slabaparola"
                    },

                     new User()
                    {
                        FirstName = "Tanya",
                        LastName = "Fikova",
                        Email = "sephora@abv.bg",
                        Password = "silnaParola"
                    }
                };

                CultureInfo provider = CultureInfo.InvariantCulture;
                CreditCard[] creditCards =
                {
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("21.06.2019", "dd.MM.yyyy", provider),
                    Limit = 2000m,
                    MoneyOwed = 50m,
                },

                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("22.07.2020", "dd.MM.yyyy", provider),
                    Limit = 3000m,
                    MoneyOwed = 500m,
                },

                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("23.08.2018", "dd.MM.yyyy", provider),
                    Limit = 6000m,
                    MoneyOwed = 3.1m,
                }
            };

                BankAccount[] bankAccounts = {
                new BankAccount()
                {
                    Balance = 1400m,
                    BankName = "BankOfSvalbard",
                    SWIFTCode = "SVLBRDMNY"
                },

                new BankAccount()
                {
                    Balance = 1565m,
                    BankName = "BankOfWrangelIsland",
                    SWIFTCode = "WRNGLGLD"
                }
            };

                PaymentMethod[] paymentMethods = new PaymentMethod[]
                {
                    new PaymentMethod
                    {
                      User = users[0],
                      CreditCard = creditCards[0],
                      Type = AccountType.CreditCard
                    },

                    new PaymentMethod
                    {
                      User = users[0],
                      CreditCard = creditCards[1],
                      Type = AccountType.CreditCard
                    },

                    new PaymentMethod
                    {
                      User = users[0],
                      BankAccount = bankAccounts[0],
                      Type = AccountType.BankAccount
                    },

                    // This causes an exception, as it should
                    //new PaymentMethod
                    //{
                    //  User = users[0],
                    //  BankAccount = bankAccounts[0],
                    //  CreditCard = creditCards[2],
                    //  Type = AccountType.BankAccount
                    //},
                };

                currentDb.Users.AddRange(users);
                currentDb.CreditCards.AddRange(creditCards);
                currentDb.BankAccounts.AddRange(bankAccounts);
                currentDb.PaymentMethods.AddRange(paymentMethods);

                currentDb.SaveChanges();
            }
        }
    }
}