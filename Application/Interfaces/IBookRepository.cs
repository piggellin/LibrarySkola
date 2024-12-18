using Domain.Models;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}
