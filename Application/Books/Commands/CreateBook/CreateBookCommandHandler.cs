using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookDto>>
    {
        private readonly IBookRepository _repo;
        private readonly ILogger<CreateBookCommandHandler> _logger;

        public CreateBookCommandHandler(IBookRepository repo, ILogger<CreateBookCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _repo.BookTitleExists(request.Book.Title))
                {
                    _logger.LogWarning("Book already exists: {BookTitle}", request.Book.Title);
                    return Result<BookDto>.Failure("Book already exists");
                }

                var newBook = new Book
                {
                    Title = request.Book.Title,
                    AuthorId = request.Book.AuthorId
                };

                await _repo.AddAsync(newBook);
                _logger.LogInformation("Book successfully added to the database: {BookTitle}", newBook.Title);

                var bookDto = new BookDto
                {
                    Title = newBook.Title,
                    AuthorId = newBook.AuthorId
                };

                return Result<BookDto>.Success(bookDto, "Book successfully added to database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the book: {BookTitle}", request.Book.Title);
                return Result<BookDto>.Failure("Unexpected error");
            }
        }

    }
}
