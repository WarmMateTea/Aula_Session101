using Microsoft.EntityFrameworkCore;

namespace Session101.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base (options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
