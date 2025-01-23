using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, Result<List<BookDto>>>
    {
        private readonly IBookRepository _repo;
        private readonly ILogger<GetAllBooksQueryHandler> _logger;

        public GetAllBooksQueryHandler(IBookRepository repo, ILogger<GetAllBooksQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<List<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = await _repo.GetAllAsync();
                if (books == null || !books.Any())
                {
                    _logger.LogWarning("No books found in the database.");
                    return Result<List<BookDto>>.Failure("No books found");
                }

                var bookDtos = books.Select(book => new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    AuthorId = book.AuthorId
                }).ToList();

                return Result<List<BookDto>>.Success(bookDtos, "Books successfully retrieved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all books.");
                throw;
            }
        }
    }
}
