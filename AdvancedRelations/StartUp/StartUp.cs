namespace P01_BillsPaymentSystem.App
{
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem;
    public class StartUp
    {
        public static void Main()
        {
            var db = new BillsPaymentSystemContext();

            using (db)
            {
            //  db.Database.EnsureDeleted();
            //  db.Database.EnsureCreated();
            //  db.Database.Migrate();

            // SeedDatabase.SeedDb(db);
               UserDetails.Get();
            }
        }
    }
}