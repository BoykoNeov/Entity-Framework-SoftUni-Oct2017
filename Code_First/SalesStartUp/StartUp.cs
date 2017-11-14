namespace SalesStartUp
{
    using P03_SalesDatabase;
    using P03_SalesDatabase.Data;
    using P03_SalesDatabase.Migrations;
    using P03_SalesDatabase.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new SalesContext();

            using (dbContext)
            {
                dbContext.Database.Migrate();
            }
        }
    }
}