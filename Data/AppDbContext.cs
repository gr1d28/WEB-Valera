using Microsoft.EntityFrameworkCore;

namespace Valera.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Valera.Models.Valera> Valeras { get; set; }
        public DbSet<Valera.Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
