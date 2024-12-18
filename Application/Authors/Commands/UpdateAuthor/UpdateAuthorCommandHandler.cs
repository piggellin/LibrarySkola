using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result<Author>>
    {
        private readonly IAuthorRepository _repo;

        public UpdateAuthorCommandHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _repo.GetByIdAsync(request.Id);
            if (author == null)
            {
                return Result<Author>.Failure($"Author with ID {request.Id} not found.");
            }

            author.Name = request.Name;
            var updated = await _repo.UpdateAsync(author);
            return Result<Author>.Success(updated);
        }
    }
}
