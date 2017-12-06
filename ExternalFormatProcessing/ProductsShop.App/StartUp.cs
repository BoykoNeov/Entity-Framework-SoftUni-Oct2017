namespace ProductsShop.App
{
    using System;
    using ProductsShop.Data;

    public class StartUp
    {
        public static void Main()
        {
            // Task 1 Also useful for reseting the db tables and filling them anew
            Console.WriteLine(@"Delete the old one (if exists) and create a new DB? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                using (ProductsShopContext dbContext = new ProductsShopContext())
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                }
            }

            Console.WriteLine("Read JSON files and fill them in db? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                Task1_ImportJSONData.ReadJsonFilesAndFillDb();
            }

            // Task 2
            Console.WriteLine("Perform task 2? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                using (var db = new ProductsShopContext())
                {
                    string productsInRange500to1000 = Task2_JsonQueryAndExportData.GetProductsInRange(db);
                    string successfullySoldProducts = Task2_JsonQueryAndExportData.GetSuccessfullySoldProducts(db);
                    string categoriesByProduct = Task2_JsonQueryAndExportData.GetCategoriesByProductsCount(db);
                    string usersAndProducts = Task2_JsonQueryAndExportData.GetUsersBySoldProducts(db);
                }
            }

            // Task 3
            Console.WriteLine("Perform task 3.1 (import XML to db)? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                using (var db = new ProductsShopContext())
                {
                    Task3_1_ImportXMLData.ImportXMLDataAndPopulateDB();
                }
            }

            Console.WriteLine("Perform task 3.2 (XML queries and export)? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                using (var db = new ProductsShopContext())
                {
                    string task3_2_1result = Task3_2_QueriesAndXMLExport.GetProductsInRange(db);
                    string task3_2_2result = Task3_2_QueriesAndXMLExport.GetSuccessfullySoldProducts(db);
                }
            }
        }
    }
}