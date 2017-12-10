using System;
using System.IO;
using FastFood.Data;
using System.Linq;
using FastFood.Models.Enums;
using System;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            OrderType thisOrderType = (OrderType)Enum.Parse(typeof(OrderType), orderType);

            //var data = context.Orders
            //    .Where(o => o.Employee.Name == employeeName && o.Type == thisOrderType)
            //    .Select(o => new
            //    {
            //        Name = o.Employee.Name,
            //        Orders = o.OrderItems.Select(oil => new
            //        {
            //            Customer = oil.Order.Customer,
            //            Items = new
            //            {
            //                oil.Item.Name,
            //                oil.Item.Price,
            //                oil.Quantity
            //            },
            //            TotalPrice = o.OrderItems.Select(oi => oi.Item.Price * oi.Quantity).Sum()
            //        }).ToArray(),




            //    }).ToArray()
            //        .OrderByDescending(oif => oif.TotalPrice)
            //        .ThenByDescending(oif => oif.Orders.Select(it => it.Items.Quantity).Sum());




            //string result = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            //return result;
            return string.Empty;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            //Use the method provided in the project skeleton, which receives a string of comma - separated category names. Export the categories: for each category, export its most popular item.The most popular item is the item from the category, which made the most money in orders.Sort the categories by the amount of money the most popular item made(descending), then by the times the item was sold(descending).

            var CategoryNames = categoriesString.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            XDocument xUsersAndPost = new XDocument(new XDeclaration("1.0", "UTF-8", null), new XElement("Categories"));

            var data = context.Categories
                .Where(e => CategoryNames.Contains(e.Name))
                .Select(e => new
                {
                    Name = e.Name,
                    MostPopularItem = e.Items.Select(er => new
                    {
                        Name = e.Items.Select(i => i.Name),
                        TotalMade = e.Items.Select(i => i.Price * i.OrderItems.Select(oi => oi.Quantity).Sum()),
                        TimesSold = e.Items.Select(i => i.OrderItems.Select(oi => oi.Quantity).Sum())
                    }).OrderByDescending(t => t.TotalMade).FirstOrDefault()
                }) 
                .OrderByDescending(e => e.MostPopularItem.TotalMade)
                .ThenByDescending(e => e.MostPopularItem.TimesSold)
                .ToArray()
                ;

            foreach (var item in data)
            {
                XElement category = new XElement("Category");
                category.Add(new XElement("Name", item.Name));

                XElement mostPopularItem = new XElement("MostPopularItem");

                XElement mostName = new XElement("Name", item.MostPopularItem.Name);
                XElement mostTotalSales = new XElement("TotalMade", item.MostPopularItem.TimesSold);
                XElement mostTotalPr = new XElement("TimesSold", item.MostPopularItem.TotalMade);

                mostPopularItem.Add(mostName);
                mostPopularItem.Add(mostTotalPr);
                mostPopularItem.Add(mostTotalSales);

                category.Add(mostPopularItem);

                xUsersAndPost.Add(category);
            }

            return xUsersAndPost.ToString();

        }
    }
}