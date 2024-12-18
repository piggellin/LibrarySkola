using Domain.Models;
using MediatR;

namespace Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Result<Book>>
    {
        public int Id { get; set; }
    }
}
