using Domain.Models;

namespace Infrastructure
{
    public class FakeDatabase
    {
        public List<Author> Authors { get; set; } = new();

        public List<Book> Books { get { return allBooksFromDb; } set { allBooksFromDb = value; } }

        private static List<Book> allBooksFromDb = new List<Book>
        {
            new Book (1, "book1", 1),
            new Book (2, "book2", 2),
            new Book (3, "book3", 3),
            new Book (4, "book4", 4)
        };

        public Book AddNewBook(Book book)
        {
            allBooksFromDb.Add(book);
            return book;
        }
    }
}
