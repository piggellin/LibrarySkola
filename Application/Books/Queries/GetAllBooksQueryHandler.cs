using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly FakeDatabase _db;

        public GetAllBooksQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_db.Books.ToList());
        }
    }
}
