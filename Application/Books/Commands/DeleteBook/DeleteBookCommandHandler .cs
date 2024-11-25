using Infrastructure;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly FakeDatabase _db;

        public DeleteBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToDelete = _db.Books.FirstOrDefault(book => book.Id == request.Id);

            if (bookToDelete == null)
            {
                return Task.FromResult(false);
            }

            _db.Books.Remove(bookToDelete);
            return Task.FromResult(true);
        }
    }
}
