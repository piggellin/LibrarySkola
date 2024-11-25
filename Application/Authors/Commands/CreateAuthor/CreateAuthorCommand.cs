using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Author>
    {
        public string Name { get; set; }
    }
}
