namespace P02_DatabaseFirst
{
    using P02_DatabaseFirst.Data;
    using System;
    using System.Linq;

    public class P08_AddressesByTown
    {
        public static void GetAddressesByTown()
        {
            SoftUniContext dbContext = new SoftUniContext();

            using (dbContext)
            {
                var addresses = dbContext.Addresses
                    .Select(a => new
                    {
                        townName = a.Town.Name,
                        employeesCount = a.Employees.Count,
                        addressText = a.AddressText
                    })
                    .OrderByDescending(a => a.employeesCount)
                    .ThenBy(a => a.townName)
                    .ThenBy(a => a.addressText)
                    .Take(10)
                    ;

                foreach (var address in addresses)
                {
                    Console.WriteLine($"{address.addressText}, {address.townName} - {address.employeesCount} employees");
                }
            }
        }
    }
}