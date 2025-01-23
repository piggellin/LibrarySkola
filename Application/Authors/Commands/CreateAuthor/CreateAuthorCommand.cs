using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Result<AuthorDto>>
    {
        public AuthorDto Author { get; set; }

        public CreateAuthorCommand(AuthorDto author)
        {
            Author = author;
        }
    }
}
