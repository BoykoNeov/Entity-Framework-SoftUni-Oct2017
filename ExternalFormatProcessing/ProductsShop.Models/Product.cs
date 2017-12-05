namespace ProductsShop.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            this.ThisProductsCategories = new HashSet<CategoryProducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public ICollection<CategoryProducts> ThisProductsCategories { get; set; }
    }
}