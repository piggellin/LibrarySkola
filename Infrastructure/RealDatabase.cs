using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class RealDatabase(DbContextOptions<RealDatabase> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Author { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-NUQVJJ3Q\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=true;");
        }
    }
}
