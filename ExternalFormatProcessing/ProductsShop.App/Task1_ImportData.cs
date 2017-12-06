namespace ProductsShop.App
{
    using Newtonsoft.Json;
    using ProductsShop.Data;
    using ProductsShop.Models;
    using System;
    using System.IO;

/// <summary>
/// Definitely not written to the standarts of quality programming code, but time is insufficient...
/// </summary>
    public static class Task1_ImportData
    {
        public static void ReadJsonFilesAndFillDb()
        {
            Random randomInstance = new Random();

            string jsonCategoriesInput = File.ReadAllText(@"../Resources/categories.json");
            string jsonUsersInput = File.ReadAllText(@"../Resources/users.json");
            string jsonProductsInput = File.ReadAllText(@"../Resources/products.json");

            User[] UsersFromJSON = JsonConvert.DeserializeObject<User[]>(jsonUsersInput);
            Category[] CategoriesFromJSON = JsonConvert.DeserializeObject<Category[]>(jsonCategoriesInput);
            Product[] ProductsFromJSON = JsonConvert.DeserializeObject<Product[]>(jsonProductsInput);

            ProductsShopContext dbContext = new ProductsShopContext();

            using (dbContext)
            {
                JSONDbFill.ImportUsersToDb(UsersFromJSON, dbContext);
                JSONDbFill.ImportCategoriesToDb(CategoriesFromJSON, dbContext);
                JSONDbFill.ImportProductsToDb(ProductsFromJSON, dbContext, randomInstance);
            }
        }
    }
}