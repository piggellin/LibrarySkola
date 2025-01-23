using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Result<BookDto>>
    {
        private readonly IBookRepository _repo;
        private readonly ILogger<GetBookByIdQueryHandler> _logger;

        public GetBookByIdQueryHandler(IBookRepository repo, ILogger<GetBookByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _repo.GetByIdAsync(request.Id);
                if (book == null)
                {
                    _logger.LogWarning("Book not found: {BookId}", request.Id);
                    return Result<BookDto>.Failure("Book not found");
                }

                var bookDto = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    AuthorId = book.AuthorId
                };

                return Result<BookDto>.Success(bookDto, "Book successfully retrieved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the book: {BookId}", request.Id);
                throw;
            }
        }
    }
}
