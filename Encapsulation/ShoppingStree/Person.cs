using System;
using System.Collections.Generic;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        public List<Product> bagOfProducts;
        private decimal money;

       public Person()
        {
            bagOfProducts = new List<Product>();
        }

        public decimal Money
        {
            get
            {
                return money;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                else
                {
                    money = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                else
                {
                    name = value;
                }
            }
        }
    }
}