using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BookDto
    {
        [Required(ErrorMessage = "Title is required")]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
    }
}
