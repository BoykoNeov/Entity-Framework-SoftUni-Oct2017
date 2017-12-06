namespace ProductsShop.App
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Data;
    using System;
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

            string result = XMLSerializer.SerializeXML(xProducts);
            // Console.WriteLine(result);
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

            string result = XMLSerializer.SerializeXML(xProducts);
            // Console.WriteLine(result);
            return result;
        }
    }
}