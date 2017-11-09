namespace P02_DatabaseFirst
{
    using P02_DatabaseFirst.Data;
    using System;
    using System.Collections.Generic;
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
                //This solution produces the correct result, but with a lot of queries
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
                                ).ToList()
                        })
                    .Take(30)
                    .ToList();


                foreach (var e in employees.ToArray())
                {
                    Console.WriteLine($"{e.employeeName} - Manager: {e.managerName}");

                    foreach (var p in e.projects)
                    {
                        const string DateTimeFormat = "M/d/yyyy h:mm:ss tt";
                        const string NullMessage = "not finished";
                        Console.WriteLine($"--{p.Name} - {p.StartDate.ToString(DateTimeFormat, CultureInfo.InvariantCulture)} - " +
                            (p.EndDate != null ? (p.EndDate.Value.ToString(DateTimeFormat, CultureInfo.InvariantCulture)) : NullMessage));
                    }
                }
            }
        }

        /// <summary>
        /// the same, using joins and only one query
        /// </summary>
        public static void PrintEmployeesAndProjects_usingJoins()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employees_2 =
                    (
                     from e in dbContext.Employees
                     .Where(e => e.EmployeesProjects.Any
                     (
                         ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003)
                      ).Take(30)
                     join epr in dbContext.EmployeesProjects on e.EmployeeId equals epr.EmployeeId
                     join p in dbContext.Projects on epr.ProjectId equals p.ProjectId
                     join m in dbContext.Employees on e.ManagerId equals m.EmployeeId
                     select new
                     {
                         p,
                         e,
                         managerName = $"{m.FirstName} {m.LastName}"
                     })
                     .ToList();

                    HashSet<string> alreadyPrintedEmployees = new HashSet<string>();
                    foreach (var employeeProject in employees_2)
                    {
                        string currentEmployeeName = $"{employeeProject.e.FirstName} {employeeProject.e.LastName}";
                        string managerName = $"{employeeProject.managerName}";

                        if (!alreadyPrintedEmployees.Contains(currentEmployeeName))
                        {
                            Console.WriteLine($"{currentEmployeeName} - Manager: {managerName}");
                            alreadyPrintedEmployees.Add(currentEmployeeName);
                        }

                        const string DateTimeFormat = "M/d/yyyy h:mm:ss tt";
                        const string NullMessage = "not finished";
                        Console.WriteLine($"--{employeeProject.p.Name} - {employeeProject.p.StartDate.ToString(DateTimeFormat, CultureInfo.InvariantCulture)} - "
                            + (employeeProject.p.EndDate != null ? (employeeProject.p.EndDate.Value.ToString(DateTimeFormat, CultureInfo.InvariantCulture)) : NullMessage));
                }
            }
        }
    }
}