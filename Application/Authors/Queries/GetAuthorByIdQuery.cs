using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAuthorByIdQuery : IRequest<Author>
    {
        public int Id { get; set; }
    }
}
