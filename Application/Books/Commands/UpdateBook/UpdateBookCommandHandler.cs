using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<BookDto>>
    {
        private readonly IBookRepository _repo;
        private readonly ILogger<UpdateBookCommandHandler> _logger;

        public UpdateBookCommandHandler(IBookRepository repo, ILogger<UpdateBookCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateBookCommand for Book Id: {BookId}", request.Book.Id);

            try
            {
                var existingBook = await _repo.GetByIdAsync(request.Book.Id);
                if (existingBook == null)
                {
                    _logger.LogWarning("Book not found: {BookId}", request.Book.Id);
                    return Result<BookDto>.Failure("Book not found");
                }

                existingBook.Title = request.Book.Title;
                existingBook.AuthorId = request.Book.AuthorId; 

                await _repo.UpdateAsync(existingBook);
                _logger.LogInformation("Book successfully updated: {BookId}", request.Book.Id);

                var bookDto = new BookDto
                {
                    Title = existingBook.Title,
                    AuthorId = existingBook.AuthorId
                };

                return Result<BookDto>.Success(bookDto, "Book successfully updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book: {BookId}", request.Book.Id);
                throw;
            }
        }
    }
}
