namespace ProductsShop.App
{
    using System;
    using ProductsShop.Data;
    using System.IO;

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
                ImportJSONData.ReadJsonFilesAndFillDb();
            }

            // Task 2
            Console.WriteLine("Perform task 2? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                using (var db = new ProductsShopContext())
                {
                    string productsInRange500to1000 = JsonQueriesAndExportData.GetProductsInRange(db);
                    string successfullySoldProducts = JsonQueriesAndExportData.GetSuccessfullySoldProducts(db);
                    string categoriesByProduct = JsonQueriesAndExportData.GetCategoriesByProductsCount(db);
                    string usersAndProducts = JsonQueriesAndExportData.GetUsersBySoldProducts(db);

                    Console.WriteLine("Write JSON queries to files?");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        File.WriteAllText(@"../Queries/query1.json", productsInRange500to1000);
                        File.WriteAllText(@"../Queries/query2.json", successfullySoldProducts);
                        File.WriteAllText(@"../Queries/query3.json", categoriesByProduct);
                        File.WriteAllText(@"../Queries/query4.json", usersAndProducts);
                    }
                }
            }

            // Task 3
            Console.WriteLine("Perform task 3.1 (import XML to db)? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                using (var db = new ProductsShopContext())
                {
                    ImportXMLData.ImportXMLDataAndPopulateDB();
                }
            }

            Console.WriteLine("Perform task 3.2 (XML queries and export)? (press 'y' for Yes)");

            if (Console.ReadLine().ToLower() == "y")
            {
                using (var db = new ProductsShopContext())
                {
                    string productsInRange = XMLQueriesAndExportData.GetProductsInRange(db);
                    string successfullySoldProducts = XMLQueriesAndExportData.GetSuccessfullySoldProducts(db);
                    string categoriesByProductsCount = XMLQueriesAndExportData.GetCategoriesByProductsCount(db);
                    string usersAndProducts = XMLQueriesAndExportData.GetUsersAndProducts(db);

                    Console.WriteLine("Write XML queries to files?");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        File.WriteAllText(@"../Queries/query1.xml", productsInRange);
                        File.WriteAllText(@"../Queries/query2.xml", successfullySoldProducts);
                        File.WriteAllText(@"../Queries/query3.xml", categoriesByProductsCount);
                        File.WriteAllText(@"../Queries/query4.xml", usersAndProducts);
                    }
                }
            }
        }
    }
}