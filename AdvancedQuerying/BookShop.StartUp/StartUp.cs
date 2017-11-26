namespace BookShop
{
    using BookShop.Data;
    using BookShop.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        static void Main()
        {
            //using (var db = new BookShopContext())
            //{
            //    DbInitializer.ResetDatabase(db);
            //}

            // GetBooksByAgeRestriction(new BookShopContext(), "minor");
            // Console.WriteLine(GetGoldenBooks(new BookShopContext()));
            // Console.WriteLine(GetBooksByPrice(new BookShopContext()));
            // Console.WriteLine(GetBooksNotRealeasedIn(new BookShopContext(), 2000));
            // Console.WriteLine(GetBooksByCategory(new BookShopContext(), "horror mystery drama"));
            // Console.WriteLine(GetBooksReleasedBefore(new BookShopContext(), "12-04-1992"));
            // Console.WriteLine(GetBookTitlesContaining(new BookShopContext(), "sK"));
            // Console.WriteLine(GetBooksByAuthor(new BookShopContext(), "r"));
            // Console.WriteLine(CountCopiesByAuthor(new BookShopContext()));
            // Console.WriteLine(GetTotalProfitByCategory(new BookShopContext()));
            // Console.WriteLine(GetMostRecentBooks(new BookShopContext()));
            // Console.WriteLine(Environment.NewLine + "**************" + Environment.NewLine);

            //Console.WriteLine(GetMostRecentBooksAlternative(new BookShopContext()));
            //Console.WriteLine(Environment.NewLine + "**************" + Environment.NewLine);

        }

        // Task 14
        public static void IncreasePrices(BookShopContext context)
        {
            List<Book> booksWhichPriceWillBeIncreased = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            booksWhichPriceWillBeIncreased.ForEach(b => b.Price += 5);
            context.SaveChanges();
        }

        // Task 15
        public static int RemoveBooks(BookShopContext context)
        {
            Book[] booksToBeRemoved = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            int booksToBeRemovedCount = booksToBeRemoved.Length;

            context.Books.RemoveRange(booksToBeRemoved);
            context.SaveChanges();

            return booksToBeRemovedCount;
        }

        // Task 13 Second alternative, ordered by category name
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var mostRecentBooks = context.Categories
                .Select(c => new
                {
                    categoryName = c.Name,
                    totalBookCount = c.CategoryBooks.Select(co => co.Book.Copies).Sum(),
                    books = c.CategoryBooks.Select(b => b.Book).OrderByDescending(b => b.ReleaseDate).Take(3).ToArray()
                })
                .OrderBy(c => c.categoryName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var category in mostRecentBooks)
            {
                sb.Append("--");
                sb.AppendLine(category.categoryName);

                foreach (Book book in category.books)
                {
                    sb.Append(book.Title);
                    sb.Append(" ");
                    sb.Append('(');
                    sb.Append(book.ReleaseDate.Value.Year);
                    sb.AppendLine(")");
                }
            }

            string result = sb.ToString().Trim();

            return result;
        }

        // Task 13 Books ordered by task definition ("The categories should be ordered by total book count")
        public static string GetMostRecentBooksAlternative(BookShopContext context)
        {
            var mostRecentBooks = context.Categories
                .Select(c => new
                {
                    categoryName = c.Name,
                    totalBookCount = c.CategoryBooks.Select(co => co.Book.Copies).Sum(),
                    books = c.CategoryBooks.Select(b => b.Book).OrderByDescending(b => b.ReleaseDate).Take(3).ToArray()
                })
                .OrderBy(c => c.totalBookCount)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach(var category in mostRecentBooks)
            {
                sb.Append("--");
                sb.AppendLine(category.categoryName);

                foreach (Book book in category.books)
                {
                    sb.Append(book.Title);
                    sb.Append('(');
                    sb.Append(book.ReleaseDate.Value.Year);
                    sb.AppendLine(")");
                }
            }

            string result = sb.ToString();

            return result;
        }

        // Task 12
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            string[] totalProfitByCategory = context.Categories
                .Select(c => new {
                    categoryName = c.Name,
                    totalCategoryProfit = c.CategoryBooks.Select(b => b.Book.Copies * b.Book.Price).Sum()
                })
                .OrderByDescending(c => c.totalCategoryProfit)
                .ThenBy(c => c.categoryName)
                .Select(c => $"{c.categoryName} ${c.totalCategoryProfit:f2}")
                .ToArray();

            string result = string.Join(Environment.NewLine, totalProfitByCategory);
            return result;
        }

            // Task 11
            public static string CountCopiesByAuthor(BookShopContext context)
        {
            string[] authorsByBooksCount = context.Authors
                .OrderByDescending(a => a.Books.Sum(b => b.Copies))
                .Select(a => a.FirstName + " " + a.LastName + " " + "-" + " " + a.Books.Sum(b => b.Copies))
                .ToArray();

            string result = string.Join(Environment.NewLine, authorsByBooksCount);
            return result;
        }

        // Task 10
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int booksWihtTitleLongerThan = context.Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksWihtTitleLongerThan;
        }

        // Task 9
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            string[] booksByAuthorLastNameStartingWith = context.Books
                .Where(b => b.Author.LastName.Substring(0, input.Length).ToLower().Equals(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();

            string result = string.Join(Environment.NewLine, booksByAuthorLastNameStartingWith);
            return result;
        }

        // Task 8
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string[] bookTitlesContaining = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(a => a)
                .ToArray();

            string result = string.Join(Environment.NewLine, bookTitlesContaining).Trim();
            return result;
        }

        // Task 7
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            string[] authorsByFirstNameEnding = context.Authors
                .Where(a => a.FirstName.Substring(a.FirstName.Length - input.Length, input.Length).Equals(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToArray();

            string result = string.Join(Environment.NewLine, authorsByFirstNameEnding);
            return result;
        }

        // Task 1
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            string[] booksByAgeRestriction = context.Books
                .Where(b => string.Equals(b.AgeRestriction.ToString(), command, StringComparison.InvariantCultureIgnoreCase))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            string result = string.Join(Environment.NewLine, booksByAgeRestriction);
            return result;
        }

        // Task 2
        public static string GetGoldenBooks(BookShopContext context)
        {
            string[] goldenBookTitles = context.Books
                    .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                    .OrderBy(b => b.BookId)
                    .Select(b => b.Title)
                    .ToArray();

            string result = string.Join(Environment.NewLine, goldenBookTitles);
            return result;
        }

        // Task 3
        public static string GetBooksByPrice(BookShopContext context)
        {
            string[] titlesAndPriceByPrice = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}")
                .ToArray();

            string result = string.Join(Environment.NewLine, titlesAndPriceByPrice);
            return result;
        }

        // Task 4
        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            string[] titlesNotReleasedIn = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            string result = string.Join(Environment.NewLine, titlesNotReleasedIn);
            return result;
        }

        // Task 5
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            HashSet<string> categoriesInput = input.ToLower().Split().ToHashSet();

            string[] booksByCategories = context.Books
                .Where(b => b.BookCategories.Select(c => c.Category.Name.ToLower()).Intersect(categoriesInput).Any())
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            string result = string.Join(Environment.NewLine, booksByCategories);
            return result;
        }

        // Task 6
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime beforeDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            string[] titlesPriceAndEditionReleasedBeforeDate = context.Books
                 .Where(b => b.ReleaseDate < beforeDate)
                 .OrderByDescending(b => b.ReleaseDate)
                 .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                 .ToArray();

            string result = string.Join(Environment.NewLine, titlesPriceAndEditionReleasedBeforeDate);
            return result;
        }
    }
}