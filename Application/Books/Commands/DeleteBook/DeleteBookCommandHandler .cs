using Application.Interfaces;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<bool>>
    {
        private readonly IBookRepository _repo;

        public DeleteBookCommandHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.DeleteAsync(request.Id);
            return success
                ? Result<bool>.Success(true)
                : Result<bool>.Failure($"Book with ID {request.Id} not found.");
        }
    }
}
