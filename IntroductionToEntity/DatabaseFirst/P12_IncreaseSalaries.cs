namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class P12_IncreaseSalaries
    {
        public static void IncreaseSalaries()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                string[] departmentsToRaiseSalaries = new string[] {"Engineering" , "Tool Design", "Marketing", "Information Services" };

                   var employeesRise = dbContext.Employees
                    .Where(e => departmentsToRaiseSalaries.Contains(e.Department.Name))
                    .ToList();

              //  Run this only once!
              //  employeesRise.ForEach(e => e.Salary *= 1.12m);
              //  dbContext.SaveChanges();

                var employees = dbContext.Employees
                    .Where(e => departmentsToRaiseSalaries.Contains(e.Department.Name))
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Salary,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToArray();
                    
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
                }

                Console.WriteLine();
            }
        }
    }
}