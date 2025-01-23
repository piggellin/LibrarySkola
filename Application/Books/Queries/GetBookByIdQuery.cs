using Application.DTOs;
using MediatR;

namespace Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Result<BookDto>>
    {
        public int Id { get; set; }

        public GetBookByIdQuery(int bookId)
        {
            Id = bookId;
        }
    }
}
