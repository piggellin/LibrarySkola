using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
