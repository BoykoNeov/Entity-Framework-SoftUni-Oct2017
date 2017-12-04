namespace ProductsShop.Models
{
    public class CategoryProducts
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CateogryId { get; set; }
        public Category Category { get; set; }
    }
}