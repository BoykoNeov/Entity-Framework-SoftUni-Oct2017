namespace ShoppingSpree
{
    using System;

    public class Product
    {
        private decimal price;

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be zero or negative");
                }
                else
                {
                    price = value;
                }
            }
        }

        private string name;

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
