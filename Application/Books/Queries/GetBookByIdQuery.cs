using Domain.Models;
using MediatR;

namespace Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }
    }
}
