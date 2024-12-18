using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Result<Book>>
    {
        private readonly IBookRepository _repo;

        public GetBookByIdQueryHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repo.GetByIdAsync(request.Id);
            return book == null
                ? Result<Book>.Failure($"Book with ID {request.Id} not found.")
                : Result<Book>.Success(book);
        }
    }
}
