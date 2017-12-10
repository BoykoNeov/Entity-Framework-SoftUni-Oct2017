namespace FastFood.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
    using FastFood.Data;
    using FastFood.Models;
    using FastFood.DataProcessor.Dto.Import;
    using FastFood.Models.Enums;
    using System.Xml.Linq;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var deserializedEmployees = JsonConvert.DeserializeObject<ImportEmployeeDTO[]>(jsonString);

            var validEmployees = new List<Employee>();
            Dictionary<string, Position> validPositionsByName = new Dictionary<string, Position>();

            foreach (ImportEmployeeDTO wannabeEmployee in deserializedEmployees)
            {
                if (!IsValid(wannabeEmployee))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!int.TryParse(wannabeEmployee.Age, out int wannabeEmployeeAge))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (wannabeEmployeeAge < 15 || wannabeEmployeeAge > 80)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (wannabeEmployee.Position.Length < 3 || wannabeEmployee.Position.Length > 30)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!validPositionsByName.ContainsKey(wannabeEmployee.Position))
                {
                    Position position = new Position()
                    {
                        Name = wannabeEmployee.Position
                    };

                    validPositionsByName.Add(position.Name, position);
                    //  context.Positions.Add(position);
                }

                Employee employee = new Employee()
                {
                    Position = validPositionsByName[wannabeEmployee.Position],
                    Name = wannabeEmployee.Name,
                    Age = wannabeEmployeeAge
                };

                validEmployees.Add(employee);
                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
            }

            context.Positions.AddRange(validPositionsByName.Values.ToArray());
            context.Employees.AddRange(validEmployees);

            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var deserializedItems = JsonConvert.DeserializeObject<ImportItemDTO[]>(jsonString);

            Dictionary<string, Item> validItemsByName = new Dictionary<string, Item>();
            Dictionary<string, Category> validCategories = new Dictionary<string, Category>();

            foreach (ImportItemDTO PotentialItem in deserializedItems)
            {
                if (!IsValid(PotentialItem))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (PotentialItem.Category.Length < 3 || PotentialItem.Category.Length > 30)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (validItemsByName.ContainsKey(PotentialItem.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!decimal.TryParse(PotentialItem.Price, out decimal itemPrice))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (itemPrice < 0.01m)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!validCategories.ContainsKey(PotentialItem.Category))
                {
                    Category newCategory = new Category()
                    {
                        Name = PotentialItem.Category
                    };

                    validCategories.Add(PotentialItem.Category, newCategory);
                }

                Item newItem = new Item()
                {
                    Name = PotentialItem.Name,
                    Category = validCategories[PotentialItem.Category],
                    Price = itemPrice
                };

                validItemsByName.Add(newItem.Name, newItem);
                sb.AppendLine(string.Format(SuccessMessage, newItem.Name));
            }

            context.Categories.AddRange(validCategories.Values.ToArray());
            context.Items.AddRange(validItemsByName.Values.ToArray());

            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, Employee> employees = context.Employees.ToDictionary(e => e.Name);
            Dictionary<string, Item> items = context.Items.ToDictionary(e => e.Name);

            XDocument xmlOrdersDoc = XDocument.Parse(xmlString);
            IEnumerable<XElement> xOrders = xmlOrdersDoc.Root.Elements();

            foreach (XElement xOrder in xOrders)
            {
                string employeeName = xOrder.Element("Employee")?.Value;
                string customerName = xOrder.Element("Customer")?.Value;

                if (string.IsNullOrWhiteSpace(customerName))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(employeeName))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!employees.ContainsKey(employeeName))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                string dateTime = xOrder.Element("DateTime")?.Value;
                if (!DateTime.TryParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime orderDateTime))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                string price = xOrder.Element("TotalPrice")?.Value;

                if (!string.IsNullOrWhiteSpace(price))
                {
                    bool parsed = decimal.TryParse(price, out decimal parsedPrice);

                    if (!parsed)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }

                    if (parsedPrice < 0.01m)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                }

                var XorderItems = xOrder.Element("Items")?.Elements();

                if (XorderItems == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                bool nonExistantOrInvalidItem = false;
                List<Item> orderItems = new List<Item>();
                Dictionary<Item, int> itemQuantities = new Dictionary<Item, int>();

                foreach (var item in XorderItems)
                {
                    string itemName = item.Element("Name")?.Value;
                    string itemQuantity = item.Element("Quantity")?.Value;

                    if (string.IsNullOrWhiteSpace(itemName) || string.IsNullOrWhiteSpace(itemQuantity))
                    {
                        nonExistantOrInvalidItem = true;
                        break;
                    }

                    if (!items.ContainsKey(itemName))
                    {
                        nonExistantOrInvalidItem = true;
                        break;
                    }

                    if (!int.TryParse(itemQuantity, out int parsedQuantity))
                    {
                        nonExistantOrInvalidItem = true;
                        break;
                    }

                    if (parsedQuantity < 1)
                    {
                        nonExistantOrInvalidItem = true;
                        break;
                    }

                    orderItems.Add(items[itemName]);

                    itemQuantities.Add(items[itemName], parsedQuantity);
                }

                if (nonExistantOrInvalidItem)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                string orderTypeString = xOrder.Element("Type")?.Value;

                OrderType orderType = OrderType.ForHere;
                Enum.TryParse(orderTypeString, out orderType);

                Employee currentEmployee = employees[employeeName];

                Order newOrder = new Order()
                {
                    Customer = customerName,
                    Employee = currentEmployee,
                    DateTime = orderDateTime,
                    Type = orderType
                };

                List<OrderItem> orderItemsList = new List<OrderItem>();
                foreach (var item in orderItems)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        Item = item,
                        Order = newOrder,
                        Quantity = itemQuantities[item]                     
                    };

                    orderItemsList.Add(orderItem);
                }

                context.Orders.Add(newOrder);
                context.SaveChanges();
                context.AddRange(orderItemsList);

                string dateOutput = newOrder.DateTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                sb.AppendLine($"Order for {newOrder.Customer} on {dateOutput} added");
                // Order for Garry on 21/08/2017 13:22 added
            }

            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}