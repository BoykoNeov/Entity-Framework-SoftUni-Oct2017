namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;
    using P02_DatabaseFirst.Data.Models;

    public class P06_AddingNewAddressAndUpdatingClass
    {
        public static void AddingNewAddressAndUpdatingClass()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                const string NewAddressText = "Vitoshka 15";
                const int NewTownId = 4;

                Address newAdress = new Address()
                {
                    AddressText = NewAddressText,
                    TownId = NewTownId
                };

                var nakov = dbContext.Employees
                    .Where(x => x.LastName == "Nakov")
                    .SingleOrDefault();

                if (nakov.Address == null || nakov.Address.AddressText != NewAddressText || nakov.Address.TownId != NewTownId)
                {
                    nakov.Address = newAdress;
                    dbContext.SaveChanges();
                }

                var emp = dbContext.Employees
                    .Select(x => new
                    {
                        x.AddressId,
                        x.Address.AddressText
                    })
                    .OrderByDescending(x => x.AddressId)
                    .Take(10);

                foreach (var e in emp)
                {
                    Console.WriteLine(e.AddressText);
                }
            }
        }
    }
}