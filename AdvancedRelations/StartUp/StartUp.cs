namespace StartUp
{
    using P01_BillsPaymentSystem;
    public class StartUp
    {
        public static void Main()
        {
            var db = new BillsPaymentSystemContext();

            using (db)
            {
                db.Database.EnsureCreated();
            }
        }
    }
}