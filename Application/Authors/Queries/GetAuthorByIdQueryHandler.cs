using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly FakeDatabase _db;

        public GetAuthorByIdQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = _db.Authors.FirstOrDefault(a => a.Id == request.Id);
            return Task.FromResult(author);
        }
    }
}
