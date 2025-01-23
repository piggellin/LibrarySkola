using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<AuthorDto>>
    {
        private readonly IAuthorRepository _repo;
        private readonly ILogger<CreateAuthorCommandHandler> _logger;

        public CreateAuthorCommandHandler(IAuthorRepository repo, ILogger<CreateAuthorCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;

        }
        public async Task<Result<AuthorDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _repo.AuthorExists(request.Author.Name))
                {
                    _logger.LogWarning("Author already exists: {AuthorName}", request.Author.Name);
                    return Result<AuthorDto>.Failure("Author already exists");
                }

                var newAuthor = new Author
                {
                    Name = request.Author.Name,
                };

                await _repo.AddAsync(newAuthor);
                _logger.LogInformation("Author successfully added to the database: {AuthorName}", newAuthor.Name);

                var authorDto = new AuthorDto
                {
                    Id = newAuthor.Id, 
                    Name = newAuthor.Name,
                };

                return Result<AuthorDto>.Success(authorDto, "Author successfully added to database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the author: {AuthorName}", request.Author.Name);
                throw;
            }
        }

    }
}
