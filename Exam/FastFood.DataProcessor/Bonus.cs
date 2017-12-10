using System;
using FastFood.Data;
using FastFood.Models;
using System.Linq;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
	    public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
            Item item = context.Items.FirstOrDefault(e => e.Name == itemName);
            
            if (item == null)
            {
                return $"Item {itemName} not found!";
            }
            else
            {
                decimal oldPrice = item.Price;
                item.Price = newPrice;
                return $"{itemName} Price updated from ${oldPrice:f2} to ${newPrice:f2}";
                //Cheeseburger Price updated from $6.00 to $6.50
            }
        }
    }
}
