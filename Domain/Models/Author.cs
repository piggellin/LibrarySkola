namespace Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public Author() { }

        public Author (int id, string name, ICollection<Book> books)
        {
            Id = id;
            Name = name;
            Books = books;
        }
    }
}
