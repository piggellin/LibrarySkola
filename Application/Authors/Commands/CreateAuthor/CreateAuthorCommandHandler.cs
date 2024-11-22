using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly FakeDatabase _db;

        public CreateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var newAuthor = new Author
            {
                Id = _db.Authors.Count + 1,
                Name = request.Name
            };

            _db.Authors.Add(newAuthor);

            return Task.FromResult(newAuthor);
        }
    }
}
