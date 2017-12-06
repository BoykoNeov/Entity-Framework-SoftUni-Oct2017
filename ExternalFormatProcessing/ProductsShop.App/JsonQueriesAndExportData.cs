namespace ProductsShop.App
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using ProductsShop.Data;
    using System.Linq;

    internal static class JsonQueriesAndExportData
    {
        /// <summary>
        /// Get all products in a specified price range:  500 to 1000 (inclusive). 
        /// Order them by price (from lowest to highest). Select only the product name, price and the full name of the seller. Export the result to JSON.
        /// </summary>
        /// <param name="context">ProductsShopContext</param>
        /// <returns>result to JSON</returns>
        internal static string GetProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                 .Where(p => p.Price >= 500 && p.Price <= 1000)

                 .Select(o => new
                 {
                     name = o.Name,
                     price = o.Price,
                     seller = o.Seller,
                 })
                 .OrderBy(a => a.price)
                 .ToArray();

            string result = JsonConvert.SerializeObject(products
                // Ignores default values (NULLs in nullables)
                , Formatting.Indented, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                }
                );
            return result;
        }

        /// <summary>
        /// Get all users who have at least 1 sold item with a buyer. Order them by last name, 
        /// then by first name. Select the person's first and last name. For each of the sold products 
        /// (products with buyers), select the product's name, price and the buyer's first and last name.
        /// </summary>
        /// <param name="context">ProductsShopContext</param>
        /// <returns>result to JSON</returns>
        internal static string GetSuccessfullySoldProducts(ProductsShopContext context)
        {
            var usersAndSoldProducts = context.Users
                .Include(u => u.ProductsSold)
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold.Where(p => p.BuyerId != null)
                                    .Select(p => new
                                    {
                                        name = p.Name,
                                        price = p.Price,
                                        buyerFirstName = p.Buyer.FirstName,
                                        byuerLastName = p.Buyer.LastName
                                    })
                })
                .ToArray();

            string result = JsonConvert.SerializeObject(usersAndSoldProducts, Formatting.Indented);
            // System.Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// Get all categories. Order them by the category’s name. For each category select its name,
        /// the number of products, the average price of those products and the total revenue (total price sum)
        /// of those products (regardless if they have a buyer or not).
        /// </summary>
        /// <param name="context">ProductsShopContext</param>
        /// <returns>result to JSON</returns>
        internal static string GetCategoriesByProductsCount(ProductsShopContext context)
        {
            var categoreisByProductsCount = context.Categories
                .Include(p => p.ThisCategoryProducts)
                .ThenInclude(cp => cp.Product)
                .OrderBy(cp => cp.Name)
                .Select(cp => new
                {
                    category = cp.Name,
                    productsCount = cp.ThisCategoryProducts.Count == 0 ? 0 : cp.ThisCategoryProducts.Select(t => t.Product).Count(),
                    averagePrice = cp.ThisCategoryProducts.Count == 0 ? 0 : cp.ThisCategoryProducts.Select(t => t.Product.Price).Average(),
                    totalRevenue = cp.ThisCategoryProducts.Count == 0 ? 0 : cp.ThisCategoryProducts.Select(t => t.Product.Price).Sum(),
                });

            string result = JsonConvert.SerializeObject(categoreisByProductsCount, Formatting.Indented);
            //   System.Console.WriteLine(result);
            return string.Empty;
        }

        /// <summary>
        /// Get all users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest), 
        /// then by last name (ascending). Select only their first and last name,
        /// age and for each product - name and price.
        ///Export the results to JSON.
        /// </summary>
        /// <param name="context">ProductsShopContext</param>
        /// <returns>results to JSON</returns>
        internal static string GetUsersBySoldProducts(ProductsShopContext context)
        {
            var usersBySoldProducts = context.Users
                                .Include(u => u.ProductsSold)
                                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                                .OrderByDescending(u => u.ProductsSold.Where(p => p.BuyerId != 0).Count())
                                .ThenBy(u => u.LastName)
                                .Select(u => new
                                {
                                    firstName = u.FirstName,
                                    lastName = u.LastName,
                                    age = u.Age,
                                    soldProducts = (new
                                    {
                                        count = u.ProductsSold.Where(pr => pr.BuyerId != 0).Count(),
                                        products = u.ProductsSold.Select(pr => new
                                        {
                                            name = pr.Name,
                                            price = pr.Price
                                        })
                                    })
                                });

            var usersToSerialize = new
            {
                usersCount = usersBySoldProducts.Count(),
                users = usersBySoldProducts
            };

            string result = JsonConvert.SerializeObject(usersToSerialize, Formatting.Indented);
         // System.Console.WriteLine(result);
            return result;
        }
    }
}