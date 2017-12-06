namespace ProductsShop.App
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    internal class Task3_2_QueriesAndXMLExport
    {
        internal static string GetProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                 .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
                 .Select(o => new
                 {
                     name = o.Name,
                     price = o.Price,
                     seller = o.Seller.FirstName == null ?  o.Seller.LastName : o.Seller.FirstName + " " + o.Seller.LastName,
                 })
                 .OrderBy(a => a.price)
                 .ToArray();

            XDocument xProducts = new XDocument(new XElement("products"));

            foreach (var item in products)
            {
                xProducts.Root.Add(new XElement("product", new XAttribute("name", item.name), new XAttribute("price", item.price), new XAttribute("seller", item.seller)));
            }

            string result = XMLSerializer.SerializeXML(xProducts, true);
            // System.Console.WriteLine(result);
            return result;
        }

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
                                        })
                    })
                    .ToArray();

            XDocument xProducts = new XDocument(new XElement("users"));

            foreach (var item in usersAndSoldProducts)
            {
                XElement user = new XElement("user");
                if (item.firstName != null)
                {
                    user.Add(new XAttribute("first-name", item.firstName));
                }

                user.Add(new XAttribute("last-name", item.lastName));

                ICollection<XElement> currentUserProducts = new List<XElement>();    
                foreach (var p in item.soldProducts)
                {
                    XElement currentProduct = new XElement("product",
                        new XElement("name", p.name),
                        new XElement("price", p.price));

                    currentUserProducts.Add(currentProduct);
                }

                XElement products = new XElement("sold-products", currentUserProducts);

                user.Add(products);
                xProducts.Root.Add(user);
            }

            string result = XMLSerializer.SerializeXML(xProducts, true);
            // System.Console.WriteLine(result);
            return result;
        }

        internal static string GetCategoriesByProductsCount(ProductsShopContext context)
        {
            XDocument xCategories = new XDocument();
            xCategories.Add(new XElement("categories"));

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

            foreach (var c in categoreisByProductsCount)
            {
                xCategories.Root.Add(new XElement("category", new XAttribute("name", c.category),
                    new XElement("products-count", c.productsCount),
                    new XElement("average-price", c.averagePrice),
                    new XElement("total-revenue", c.totalRevenue)));
            }

            string result = XMLSerializer.SerializeXML(xCategories, true);
            System.Console.WriteLine(result);
            return result;
        }
    }
}