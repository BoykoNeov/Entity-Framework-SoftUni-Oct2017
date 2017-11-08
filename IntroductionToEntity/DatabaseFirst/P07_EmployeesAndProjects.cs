namespace P02_DatabaseFirst
{
    using P02_DatabaseFirst.Data;
    using System;
    using System.Globalization;
    using System.Linq;

    public class P07_EmployeesAndProjects
    {
        /// <summary>
        /// Find the first 30 employees who have projects started in the period 2001 - 2003 (inclusive). 
        /// Print each employee's first name, last name, manager’s first name and last name. 
        /// Then print all of their projects in the format "--<ProjectName> - <StartDate> - <EndDate>", each on a new row. 
        /// If a project has no end date, print "not finished" instead.
        /// </summary>
        public static void PrintEmployeesAndProjects()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                // not the best way to solve this problem (and also i havent properly understood the requirement to include ALL projects, not only those starting between
                // 2001 and 2003 - this is a condition only for the employee
                // var employees = (
                //                from e in dbContext.Employees.Take(30)
                //                join ep in dbContext.EmployeesProjects on e.EmployeeId equals ep.EmployeeId
                //                join p in dbContext.Projects on ep.ProjectId equals p.ProjectId
                //                where (p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
                //                select new
                //                {
                //                    employeeFirstName = e.FirstName,
                //                    employeeLastName = e.LastName,
                //                    managerFirstName = e.Manager.FirstName,
                //                    managerLastName = e.Manager.LastName,
                //                    projectName = p.Name,
                //                    projectStartDate = p.StartDate,
                //                    projectEndDate = p.EndDate
                //                }
                //                 );

                var employees = dbContext.Employees
                    .Where
                    (
                        e => e.EmployeesProjects.Any
                     (
                            ep =>
                                ep.Project.StartDate.Year >= 2001 &&
                                ep.Project.StartDate.Year <= 2003
                    ))
                    .Select
                        (e => new
                        {
                            employeeName = $"{e.FirstName} {e.LastName}",
                            managerName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                            projects = e.EmployeesProjects.Select
                                (ep => new
                                {
                                    ep.Project.Name,
                                    ep.Project.StartDate,
                                    ep.Project.EndDate
                                }
                                )
                        })
                    .Take(30);

                foreach (var e in employees.ToArray())
                {
                    Console.WriteLine($"{e.employeeName} - Manager: {e.managerName}");

                    foreach (var p in e.projects)
                    {
                        const string dateTimeFormat = "M/d/yyyy h:mm:ss tt";
                        const string nullMessage = "not finished";
                        Console.WriteLine($"--{p.Name} - {p.StartDate.ToString(dateTimeFormat, CultureInfo.InvariantCulture)} - " +
                            (p.EndDate != null ? (p.EndDate.Value.ToString(dateTimeFormat, CultureInfo.InvariantCulture)) : nullMessage));
                    }
                }

                Console.WriteLine();

            }
        }
    }
}