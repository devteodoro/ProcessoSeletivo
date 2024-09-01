using Microsoft.EntityFrameworkCore;
using ProcessoSeletivo.Domain.Models;
using ProcessoSeletivo.Infrastructure.Data.Mappings;

namespace ProcessoSeletivo.Infrastructure.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);
        }
    }
}
