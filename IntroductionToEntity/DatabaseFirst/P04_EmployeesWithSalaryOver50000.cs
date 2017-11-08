namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class P04_EmployeesWithSalaryOver50000
    {
        public static void GetEmployessWithSalaryOver50000()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var emp = dbContext.Employees.Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                  .Where(e => e.Salary > 50000)
                  .OrderBy(e => e.FirstName)
                  .AsEnumerable();

                foreach (var e in emp)
                {
                    Console.WriteLine(e.FirstName);
                }
            }
        }
    }
}