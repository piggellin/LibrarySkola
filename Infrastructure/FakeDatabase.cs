using Domain.Models;

namespace Infrastructure
{
    public class FakeDatabase
    {
        public List<Book> Books { get; set; } = new();
        public List<Author> Authors { get; set; } = new();

        public FakeDatabase()
        {
            Authors.Add(new Author { Id = 1, Name = "Emil" });
            Books.Add(new Book { Id = 1, Title = "Book", AuthorId = 1 });
        }
    }
}
