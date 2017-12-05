﻿namespace ProductsShop.App
{
    using System;
    using ProductsShop.Data;

    public class StartUp
    {
        public static void Main()
        {
            Console.WriteLine(@"Delete the old one (if exists) and create a new DB, then read JSON files and fill them in db (Sure, that far from best practice) ? (press 'y' for Yes)");
            if (Console.ReadLine().ToLower() == "y")
            {
                Task1_ImportData.ReadJsonFilesAndFillDb();
            }

            var db = new ProductsShopContext();

            using (db)
            {
                string productsInRange500to1000 = Task2_JsonQueryAndExportData.GetProductsInRange(db);
                string successfullySoldProducts = Task2_JsonQueryAndExportData.GetSuccessfullySoldProducts(db);
                string categoriesByProduct = Task2_JsonQueryAndExportData.GetCategoriesByProductsCount(db);
                string usersAndProducts = Task2_JsonQueryAndExportData.GetUsersBySoldProducts(db);
            }


        }
    }
}