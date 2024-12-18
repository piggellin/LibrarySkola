using Domain.Models;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly RealDatabase _context;

        public AuthorRepository(RealDatabase context)
        {
            _context = context;
        }

        public async Task<Author> AddAsync(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null) return false;
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Author.FindAsync(id);
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Author.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }
    }

}
