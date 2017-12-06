namespace ProductsShop.App
{
    using ProductsShop.Data;
    using ProductsShop.Models;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public static class DbFill
    {
        /// <summary>
        /// Imports Users to ProductShop database
        /// </summary>
        /// <param name="users">PhotoShareContext Users</param>
        /// <param name="context">ProductsShopContext</param>
        public static void ImportUsersToDb(IEnumerable<User> users, ProductsShopContext context)
        {
            context.Users.AddRange(users);

            context.SaveChanges();
        }

        /// <summary>
        /// Imports Categories to ProductShop database
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="context"></param>
        public static void ImportCategoriesToDb(IEnumerable<Category> categories, ProductsShopContext context)
        {
            context.Categories.AddRange(categories);

            context.SaveChanges();
        }

        /// <summary>
        /// Important! * DB should already contain users and categories, in order for 'ImportProductsToDb to function properly'
        /// Imports products to ProductShop database
        /// </summary>
        /// <param name="products">Ienumarable of products for ProductShop</param>
        /// <param name="context">ProductShop db context</param>
        /// <param name="rnd">instance of System.Random</param>
        public static void ImportProductsToDb(IEnumerable<Product> products, ProductsShopContext context, Random rnd)
        {
            HashSet<int> usersIDs = context.Users
                .Select(e => e.Id)
                .ToHashSet();

            HashSet<int> categoriesIDs = context.Categories
                .Select(e => e.Id)
                .ToHashSet();

            int minimalUserId = usersIDs.Min();
            int maximalUserId = usersIDs.Max();
            int minimalCategoryId = categoriesIDs.Min();
            int maximalCategoryId = categoriesIDs.Max();

            foreach (Product product in products)
            {
                while (true)
                {
                    int randomUserId = rnd.Next(minimalUserId, maximalUserId + 1);

                    if (usersIDs.Contains(randomUserId))
                    {
                        product.SellerId = randomUserId;
                        break;
                    }
                }

                // 10 percent chance for BuyerId to remain null
                if (rnd.Next(0, 10) != 0)
                {
                    while (true)
                    {
                        int randomUserId = rnd.Next(minimalUserId, maximalUserId + 1);

                        if (usersIDs.Contains(randomUserId) && product.SellerId != randomUserId)
                        {
                            product.BuyerId = randomUserId;
                            break;
                        }
                    }
                }

                // Generate 1 to 3 categories for the product
                for (int i = 0; i < rnd.Next(1, 4); i++)
                {
                    while (true)
                    {
                        int randomCategoryId = rnd.Next(minimalCategoryId, maximalCategoryId + 1);

                        if (categoriesIDs.Contains(randomCategoryId))
                        {
                            CategoryProducts newProductCategory = new CategoryProducts
                            {
                                ProductId = product.Id,
                                CateogryId = randomCategoryId
                            };

                            if (!product.ThisProductsCategories.Contains(newProductCategory))
                            {
                                product.ThisProductsCategories.Add(newProductCategory);
                                break;
                            }
                        }
                    }
                }
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}