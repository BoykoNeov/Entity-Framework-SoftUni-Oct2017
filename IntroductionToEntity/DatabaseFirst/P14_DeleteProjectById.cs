namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class P14_DeleteProjectById
    {
        public static void DeleteProjectById()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var project = dbContext.EmployeesProjects
                    .Where(ep => ep.ProjectId == 2)
                    .ToList();
                dbContext.EmployeesProjects.RemoveRange(project);

                var projectFromProjects = dbContext.Projects
                    .Where(p => p.ProjectId == 2)
                    .ToList();
                dbContext.Projects.RemoveRange(projectFromProjects);

                dbContext.SaveChanges();

                var remainingProjects = dbContext.Projects
                    .Take(10)
                    .Select(p => p.Name)
                    .ToList();

                foreach (var remainingProject in remainingProjects)
                {
                    Console.WriteLine(remainingProject);
                }
            }
        }
    }
}