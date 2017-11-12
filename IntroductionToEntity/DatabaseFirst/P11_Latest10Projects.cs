namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;
    using System.Globalization;

    public class P11_Latest10Projects
    {
        public static void GetLatest10Projects()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var projects = dbContext.Projects
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name)
                    .Select(p => new
                    {
                        name = p.Name,
                        description = p.Description,
                        startDate = p.StartDate
                    })
                    .ToArray();

                foreach (var project in projects)
                {
                    Console.WriteLine(project.name);
                    Console.WriteLine(project.description);
                    Console.WriteLine(project.startDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}