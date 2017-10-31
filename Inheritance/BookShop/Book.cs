namespace BookShop
{
    using System;

    public class Book
    {
        private string title;
        private string author;
        private decimal price;

        public Book(string inputAuthor, string inputTitle, decimal inputPrice)
        {
            this.Title = inputTitle;
            this.Price = inputPrice;
            this.Author = inputAuthor;
        }

        public virtual decimal Price
        {
            get
            {
                return price;
            }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price not valid!");
                }
                price = value;
            }
        }

        public string Author
        {
            get
            {
                return this.author;
            }
            protected set
            {
                string[] authorNames = value.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                string authorSecondName = string.Empty;
                if (authorNames.Length > 1)
                {
                    authorSecondName = authorNames[1];
                    if (char.IsDigit(authorSecondName[0]))
                    {
                        throw new ArgumentException("Author not valid!");
                    }
                }

                this.author = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            protected set
            {
                const int minimumTitleLength = 3;

                if (value.Length < minimumTitleLength)
                {
                    throw new ArgumentException("Title not valid!");
                }
                title = value;
            }
        }

        public override string ToString()
        {
            return $"Type: {this.GetType().Name}" + Environment.NewLine +
                $"Title: {this.Title}" + Environment.NewLine +
                $"Author: {this.Author}" + Environment.NewLine +
                $"Price: {this.Price:f2}";
        }
    }
}