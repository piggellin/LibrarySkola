using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<Author>>>
    {
        private readonly IAuthorRepository _repo;

        public GetAllAuthorsQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _repo.GetAllAsync();
            return Result<List<Author>>.Success(authors);
        }
    }
}
