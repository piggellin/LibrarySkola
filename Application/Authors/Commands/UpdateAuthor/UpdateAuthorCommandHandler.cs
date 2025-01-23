using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result<AuthorDto>>
    {
        private readonly IAuthorRepository _repo;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger;

        public UpdateAuthorCommandHandler(IAuthorRepository repo, ILogger<UpdateAuthorCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<AuthorDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateAuthorCommand for Author Id: {AuthorId}", request.Author.Id);

            try
            {
                var existingAuthor = await _repo.GetByIdAsync(request.Author.Id);
                if (existingAuthor == null)
                {
                    _logger.LogWarning("Author not found: {AuthorId}", request.Author.Id);
                    return Result<AuthorDto>.Failure("Author not found");
                }

                existingAuthor.Name = request.Author.Name;

                await _repo.UpdateAsync(existingAuthor);
                _logger.LogInformation("Author successfully updated: {AuthorId}", request.Author.Id);

                var authorDto = new AuthorDto
                {
                    Id = existingAuthor.Id,
                    Name = existingAuthor.Name,
                };

                return Result<AuthorDto>.Success(authorDto, "Author successfully updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the author: {AuthorId}", request.Author.Id);
                throw;
            }
        }
    }
}
