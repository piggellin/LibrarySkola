
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AuthorDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public int Id { get; set; }
    }
}
