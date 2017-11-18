namespace StartUp
{
    using P01_BillsPaymentSystem;
    using Microsoft.EntityFrameworkCore;
    public class StartUp
    {
        public static void Main()
        {
            var db = new BillsPaymentSystemContext();

            using (db)
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }
        }
    }
}