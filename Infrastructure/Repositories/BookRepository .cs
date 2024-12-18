using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RealDatabase _context;

        public BookRepository(RealDatabase context)
        {
            _context = context;
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
