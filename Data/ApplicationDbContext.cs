using Microsoft.EntityFrameworkCore;
using myMeow2.Models;
namespace myMeow2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Gatinho> Gatinhos { get; set; }
        public DbSet<Adotante> Adotantes { get; set; }
        public DbSet<Adocao> Adocoes { get; set; }
    }
}

