namespace BookShop
{
    internal class GoldenEditionBook : Book
    {
        public override decimal Price
        {
            get
            {
                return base.Price;
            }
            protected set
            {
                base.Price = value * 1.3m;
            }
        }

        public GoldenEditionBook(string inputAuthor, string inputTitle, decimal inputPrice) : base(inputAuthor, inputTitle, inputPrice)
        {
        }
    }
}