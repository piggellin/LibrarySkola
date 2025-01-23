using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Authors.Queries
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Result<AuthorDto>>
    {
        private readonly IAuthorRepository _repo;
        private readonly ILogger<GetAuthorByIdQueryHandler> _logger;

        public GetAuthorByIdQueryHandler(IAuthorRepository repo, ILogger<GetAuthorByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _repo.GetByIdAsync(request.AuthorId);
                if (author == null)
                {
                    _logger.LogWarning("Author not found: {AuthorId}", request.AuthorId);
                    return Result<AuthorDto>.Failure("Author not found");
                }

                var authorDto = new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name
                };

                return Result<AuthorDto>.Success(authorDto, "Author successfully retrieved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the author: {AuthorId}", request.AuthorId);
                throw;
            }
        }
    }
}
