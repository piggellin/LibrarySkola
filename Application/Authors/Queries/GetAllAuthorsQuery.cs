using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAllAuthorsQuery : IRequest<Result<List<Author>>> { }
}
