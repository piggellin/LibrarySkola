using Domain.Models;

namespace Application.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<bool> AuthorExists(string name);
    }
}
