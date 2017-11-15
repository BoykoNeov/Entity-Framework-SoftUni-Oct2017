namespace P01_Studentsystem
{
    using P01_StudentSystem.Data;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new StudentSystemContext();

            using (dbContext)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}