namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class P09_Employee147
    {
        public static void GetEmployee147()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employee147 = dbContext.Employees
                    .Where(e => e.EmployeeId == 147)
                    .Select(e => new
                    {
                        employeeName = ($"{e.FirstName} {e.LastName}"),
                        jobPosition = e.JobTitle,
                        projects = e.EmployeesProjects
                        .Select( p => new
                        {
                            p.Project.Name
                        }
                        )
                        .OrderBy(p => p.Name)
                        })
                        .Single();

                Console.WriteLine($"{employee147.employeeName} - {employee147.jobPosition}");
                foreach (var project in employee147.projects)
                {
                    Console.WriteLine($"{project.Name}");
                }
            }
        }
    }
}