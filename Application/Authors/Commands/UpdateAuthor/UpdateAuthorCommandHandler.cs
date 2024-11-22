using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly FakeDatabase _db;

        public UpdateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToUpdate = _db.Authors.FirstOrDefault(author => author.Id == request.Id);

            if (authorToUpdate == null)
            {
                throw new Exception($"Author with ID {request.Id} not found.");
            }

            authorToUpdate.Name = request.Name;

            return Task.FromResult(authorToUpdate);
        }
    }
}
