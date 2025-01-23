using Application.DTOs;
using MediatR;

namespace Application.Books.Queries
{
    public class GetAllBooksQuery : IRequest<Result<List<BookDto>>>{}
}
