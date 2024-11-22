using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly FakeDatabase _db;

        public GetAllAuthorsQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_db.Authors.ToList());
        }
    }
}
