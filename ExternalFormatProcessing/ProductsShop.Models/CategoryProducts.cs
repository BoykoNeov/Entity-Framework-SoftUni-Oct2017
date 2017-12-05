namespace ProductsShop.Models
{
    public class CategoryProducts
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CateogryId { get; set; }
        public Category Category { get; set; }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + ProductId.GetHashCode();
            hash = (hash * 7) + CateogryId.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            var item = obj as CategoryProducts;

            if (item == null)
            {
                return false;
            }

            return (this.CateogryId == item.CateogryId && this.ProductId == item.ProductId);
        }
    }
}