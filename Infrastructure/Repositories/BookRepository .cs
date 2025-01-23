using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository(RealDatabase context) : Repository<Book>(context), IBookRepository
    {
        private readonly RealDatabase _context = context;
        public async Task<bool> BookTitleExists(string title)
        {
            return await _context.Books.AnyAsync(a => a.Title == title);
        }
    }
}
