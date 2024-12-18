using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Result<Author>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
