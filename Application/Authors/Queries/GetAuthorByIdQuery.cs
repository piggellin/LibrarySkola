using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAuthorByIdQuery : IRequest<Result<Author>>
    {
        public int Id { get; set; }
    }
}
