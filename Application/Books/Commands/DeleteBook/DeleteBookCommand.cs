using Domain.Models;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
