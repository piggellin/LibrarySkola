using Domain.Models;

namespace Application.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> BookTitleExists(string title);
    }
}
