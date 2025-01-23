using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<bool>>
    {
        private readonly IAuthorRepository _repo;
        private readonly ILogger<DeleteAuthorCommandHandler> _logger;

        public DeleteAuthorCommandHandler(IAuthorRepository repo, ILogger<DeleteAuthorCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _repo.GetByIdAsync(request.AuthorId);
                if (author == null)
                {
                    _logger.LogWarning("Author not found: {AuthorId}", request.AuthorId);
                    return Result<bool>.Failure("Author not found");
                }

                await _repo.DeleteAsync(request.AuthorId);
                _logger.LogInformation("Author successfully deleted: {AuthorId}", request.AuthorId);

                return Result<bool>.Success(true, "Author successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the author: {AuthorId}", request.AuthorId);
                throw;
            }
        }
    }
}
