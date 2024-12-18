using Application.Interfaces;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<bool>>
    {
        private readonly IAuthorRepository _repo;

        public DeleteAuthorCommandHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.DeleteAsync(request.Id);
            return success
                ? Result<bool>.Success(true)
                : Result<bool>.Failure($"Author with ID {request.Id} not found.");
        }
    }
}
