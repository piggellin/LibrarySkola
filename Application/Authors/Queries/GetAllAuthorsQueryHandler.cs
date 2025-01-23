using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Authors.Queries
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<AuthorDto>>>
    {
        private readonly IAuthorRepository _repo;
        private readonly ILogger<GetAllAuthorsQueryHandler> _logger;

        public GetAllAuthorsQueryHandler(IAuthorRepository repo, ILogger<GetAllAuthorsQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<List<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await _repo.GetAllAsync();
                if (authors == null || !authors.Any())
                {
                    _logger.LogWarning("No authors found in the database.");
                    return Result<List<AuthorDto>>.Failure("No authors found");
                }

                var authorDtos = authors.Select(author => new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name
                }).ToList();

                return Result<List<AuthorDto>>.Success(authorDtos, "Authors successfully retrieved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all authors.");
                throw;
            }
        }
    }
}
