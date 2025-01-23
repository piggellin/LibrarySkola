using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Result<bool>>
    {
        public int AuthorId { get; set; }

        public DeleteAuthorCommand(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
