using Domain.Models;

namespace Infrastructure
{
    public class FakeDatabase
    {
        public List<Book> Books { get; } = new List<Book>
        {
            new Book { Id = 1, Title = "Sample Book", AuthorId = 1 }
        };

        public List<Author> Authors { get; } = new List<Author>
        {
            new Author { Id = 1, Name = "Sample Author" }
        };

        private static List<Author> allAuthors = new List<Author>
        {
            new Author (1, "Author1", allBooksFromDb),
            new Author (2, "Author2", allBooksFromDb),
        };

        private static List<Book> allBooksFromDb = new List<Book>
        {
            new Book (1, "book1", 1),
            new Book (2, "book2", 2),
            new Book (3, "book3", 3),
            new Book (4, "book4", 4)
        }; 
    }
}
