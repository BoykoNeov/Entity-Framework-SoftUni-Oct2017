namespace ProductsShop.App
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    using System.Collections.Generic;
    using ProductsShop.Models;
    using ProductsShop.Data;

    internal static class ImportXMLData
    {
        internal static void ImportXMLDataAndPopulateDB()
        {
            string xmlUsersInput = File.ReadAllText(@"../Resources/users.xml");
            XDocument xmlUsersDoc = XDocument.Parse(xmlUsersInput);
            IEnumerable<XElement> xElementsUsers = xmlUsersDoc.Root.Elements();
            List<User> users = new List<User>();

            foreach (XElement e in xElementsUsers)
            {
                string firstName = e.Attribute("firstName")?.Value;
                string lastName = e.Attribute("lastName").Value;

                bool successfullyParsed = int.TryParse(e.Attribute("age")?.Value, out int parsedAge);
                int? age = successfullyParsed ? parsedAge : (int?)null;

                User user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };

                users.Add(user);
            }

            string xmlCategoriesInput = File.ReadAllText(@"../Resources/categories.xml");
            XDocument xmlCategoriesDoc = XDocument.Parse(xmlCategoriesInput);
            IEnumerable<XElement> xElementsCategories = xmlCategoriesDoc.Root.Elements();
            List<Category> categories = new List<Category>();

            foreach (XElement e in xElementsCategories)
            {
                Category category = new Category()
                {
                    Name = e.Element("name").Value
                };

                categories.Add(category);
            }

            string xmlProductsInput = File.ReadAllText(@"../Resources/products.xml");
            XDocument xmlProductsDoc = XDocument.Parse(xmlProductsInput);
            IEnumerable<XElement> xProductsCategories = xmlProductsDoc.Root.Elements();
            List<Product> products = new List<Product>();

            foreach (XElement e in xProductsCategories)
            {
                Product product = new Product()
                {
                    Name = e.Element("name").Value,
                    Price = decimal.Parse(e.Element("price").Value)
                };

                products.Add(product);
            }

            using (ProductsShopContext context = new ProductsShopContext())
            {
                DbFill.ImportUsersToDb(users, context);
                DbFill.ImportCategoriesToDb(categories, context);
                Random randomInstance = new Random();
                DbFill.ImportProductsToDb(products, context, randomInstance);
            }
        }
    }
}