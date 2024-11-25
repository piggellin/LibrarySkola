namespace Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }   
        public int AuthorId { get; set; }

        public Book() { } 

        public Book(int id, string title, int authorId)
        {
            Id = id;
            Title = title;
            AuthorId = authorId;
        }
    }
}
