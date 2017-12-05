namespace ProductsShop.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.ProductsBought = new HashSet<Product>();
            this.ProductsSold = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }

        public ICollection<Product> ProductsBought { get; set; }
        public ICollection<Product> ProductsSold { get; set; }
    }
}