namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class P05_EmployeesFromResearchAndDevelopment
    {
        public static void GetEmployeesFromResearchAndDevelopment()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employees = dbContext.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Salary,
                        e.Department
                    })
                    .Where (e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName);

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}");
                }
            }
        }
    }
}