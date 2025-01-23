using Domain.Models;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class AuthorRepository(RealDatabase context) : Repository<Author>(context), IAuthorRepository
    {
        private readonly RealDatabase _context = context;

        public async Task<bool> AuthorExists(string name)
        {
            return await _context.Authors.AnyAsync(a => a.Name == name);
        }
    }

}
