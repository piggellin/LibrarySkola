using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly FakeDatabase _db;

        public UpdateBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = _db.Books.FirstOrDefault(book => book.Id == request.Id);

            if (bookToUpdate == null)
            {
                throw new Exception($"Book with ID {request.Id} not found.");
            }

            bookToUpdate.Title = request.Title;
            bookToUpdate.AuthorId = request.AuthorId;

            return Task.FromResult(bookToUpdate);
        }
    }
}
