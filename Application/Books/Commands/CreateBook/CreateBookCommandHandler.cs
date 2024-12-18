using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<Book>>
    {
        private readonly IBookRepository _repo;

        public CreateBookCommandHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = new Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId
            };

            var created = await _repo.AddAsync(newBook);
            return Result<Book>.Success(created);
        }
    }
}
