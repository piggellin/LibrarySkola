using Domain.Models;
using Infrastructure;
using MediatR;

namespace Application.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly FakeDatabase _db;

        public GetBookByIdQueryHandler(FakeDatabase db) => _db = db;

        public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(_db.Books.FirstOrDefault(b => b.Id == request.Id));
    }
}
