using Application.DTOs;
using MediatR;

namespace Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Result<BookDto>>
    {
        public int BookId { get; set; }

        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }
    }
}
