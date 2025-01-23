using Domain.Models;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<Result<bool>>
    {
        public int BookId { get; set; }

        public DeleteBookCommand(int bookId)
        {
            BookId = bookId;
        }
    }
}
