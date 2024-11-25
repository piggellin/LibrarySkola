using Infrastructure;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly FakeDatabase _db;

        public DeleteAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToDelete = _db.Authors.FirstOrDefault(author => author.Id == request.Id);

            if (authorToDelete == null)
            {
                return Task.FromResult(false); 
            }
            _db.Books.RemoveAll(book => book.AuthorId == authorToDelete.Id);

            _db.Authors.Remove(authorToDelete);
            return Task.FromResult(true);
        }
    }
}
