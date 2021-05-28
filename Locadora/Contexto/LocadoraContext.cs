using Locadora.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Data
{
    public class LocadoraContext : DbContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options)
            : base(options)
        {
        }

        public DbSet<Filme> Filme { get; set; }

        public DbSet<Genero> Genero { get; set; }

        public DbSet<Locacao> Locacao { get; set; }
    }
}
