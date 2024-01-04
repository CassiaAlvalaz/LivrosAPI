using LivrosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrosAPI.Data;

public class LivrosContext : DbContext
{
    public LivrosContext(DbContextOptions<LivrosContext> opts) : base(opts)
    {
            
    }

    public DbSet<Livro> Livros { get; set; }
}
