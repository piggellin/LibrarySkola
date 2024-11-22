using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly FakeDatabase _db;

        public CreateBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = new Book
            {
                Id = _db.Books.Count + 1,
                Title = request.Title,
                AuthorId = request.AuthorId
            };

            _db.Books.Add(newBook);

            return Task.FromResult(newBook);
        }
    }
}
