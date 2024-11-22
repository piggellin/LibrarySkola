using Domain.Models;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
    }
}
