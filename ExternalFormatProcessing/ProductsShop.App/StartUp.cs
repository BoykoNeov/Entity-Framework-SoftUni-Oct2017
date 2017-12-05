namespace ProductsShop.App
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Data;

    public class StartUp
    {
        public static void Main()
        {
            ProductsShopContext db = new ProductsShopContext();

            using (db)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}