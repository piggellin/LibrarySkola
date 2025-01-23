using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<bool>>
    {
        private readonly IBookRepository _repo;
        private readonly ILogger<DeleteBookCommandHandler> _logger;

        public DeleteBookCommandHandler(IBookRepository repo, ILogger<DeleteBookCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteBookCommand for Book Id: {BookId}", request.BookId);

            try
            {
                var book = await _repo.GetByIdAsync(request.BookId);
                if (book == null)
                {
                    _logger.LogWarning("Book not found: {BookId}", request.BookId);
                    return Result<bool>.Failure("Book not found");
                }

                await _repo.DeleteAsync(request.BookId);
                _logger.LogInformation("Book successfully deleted: {BookId}", request.BookId);

                return Result<bool>.Success(true, "Book successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book: {BookId}", request.BookId);
                throw;
            }
        }
    }
}
