using Domain.Models;
using MediatR;

namespace Application.Books.Queries
{
    public class GetAllBooksQuery : IRequest<Result<List<Book>>> { }
}
