using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, Result<List<Book>>>
    {
        private readonly IBookRepository _repo;

        public GetAllBooksQueryHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repo.GetAllAsync();
            return Result<List<Book>>.Success(books);
        }
    }
}
