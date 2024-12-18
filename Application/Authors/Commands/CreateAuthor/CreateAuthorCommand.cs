using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Result<Author>>
    {
        public string Name { get; set; }
    }
}
