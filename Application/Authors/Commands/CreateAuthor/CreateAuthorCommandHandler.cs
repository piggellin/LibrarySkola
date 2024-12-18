using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<Author>>
    {
        private readonly IAuthorRepository _repo;

        public CreateAuthorCommandHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author { Name = request.Name };
            var created = await _repo.AddAsync(author);
            return Result<Author>.Success(created);
        }
    }
}
