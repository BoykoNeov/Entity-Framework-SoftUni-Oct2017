using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P02_DatabaseFirst
{
    public class P03_EmployeesFullInfo
    {
        public static void GetEmployeesFullInfo()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var emp = dbContext.Employees.Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                }).AsEnumerable()
                  .OrderBy(e => e.EmployeeId);

                foreach (var e in emp)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
                }
            }
        }
    }
}