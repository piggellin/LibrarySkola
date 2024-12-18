using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Result<Author>>
    {
        private readonly IAuthorRepository _repo;

        public GetAuthorByIdQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _repo.GetByIdAsync(request.Id);
            if (author == null)
                return Result<Author>.Failure($"Author with ID {request.Id} not found.");

            return Result<Author>.Success(author);
        }
    }
}
