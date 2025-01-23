using Application.DTOs;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Result<AuthorDto>>
    {
        public AuthorDto Author { get; set; }

        public UpdateAuthorCommand(AuthorDto author)
        {
            Author = author;
        }
    }
}
