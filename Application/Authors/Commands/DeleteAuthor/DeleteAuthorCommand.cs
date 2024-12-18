using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
