using Domain.Models;

namespace Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<Author> AddAsync(Author author);
        Task<bool> DeleteAsync(int id);
        Task<Author> UpdateAsync(Author author);
    }
}
