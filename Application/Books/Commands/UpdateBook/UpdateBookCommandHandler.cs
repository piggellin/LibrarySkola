using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<Book>>
    {
        private readonly IBookRepository _repo;

        public UpdateBookCommandHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repo.GetByIdAsync(request.Id);
            if (book == null)
            {
                return Result<Book>.Failure($"Book with ID {request.Id} not found.");
            }

            book.Title = request.Title;
            book.AuthorId = request.AuthorId;

            var updated = await _repo.UpdateAsync(book);
            return Result<Book>.Success(updated);
        }
    }
}
