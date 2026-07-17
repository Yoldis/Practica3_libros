using Microsoft.EntityFrameworkCore;
using Practica3_libros.Models;

namespace Practica3_libros.Context
{
    public class LibrosDbContext: DbContext
    {
        public LibrosDbContext(DbContextOptions<LibrosDbContext> options) : base(options)
        {
        }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<Autor> Autor { get; set; }
    }
}
