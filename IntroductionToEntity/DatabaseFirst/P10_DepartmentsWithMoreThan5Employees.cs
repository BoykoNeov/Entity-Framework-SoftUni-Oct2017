namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;
    using Microsoft.EntityFrameworkCore;

    public class P10_DepartmentsWithMoreThan5Employees
    {
        public static void GetDepartmentsWithMoreThan5Employees()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var departments = dbContext.Departments
                    .Include(d => d.Employees)
                    .Where(d => d.Employees.Count > 5).ToArray()
                    .OrderBy(d => d.Employees.Count)
                    .ThenBy(d => d.Name);

                foreach (var department in departments)
                {
                    Console.WriteLine($"{department.Name} - {department.Manager.FirstName} {department.Manager.LastName}");

                    foreach (var employee in department.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                    {
                        Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                    }

                    Console.WriteLine(new string('-', 10));
                }

                Console.WriteLine();
            }
        }
    }
}