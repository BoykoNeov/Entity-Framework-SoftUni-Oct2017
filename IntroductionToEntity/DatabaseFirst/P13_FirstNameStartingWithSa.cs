namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class P13_FirstNameStartingWithSa
    {
        public static void GetEmployeesWithFirstNameStartingWithSa()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employees = dbContext.Employees
                 .Where(e => e.FirstName.Substring(0, 2).Equals("Sa"))
                 .Select(e => new
                 {
                     e.FirstName,
                     e.LastName,
                     e.JobTitle,
                     e.Salary
                 })
                 .OrderBy(e => e.FirstName)
                 .ThenBy(e => e.LastName)
                 .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
                }

                Console.WriteLine();
            }
        }
    }
}