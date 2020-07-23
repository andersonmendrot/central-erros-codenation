using Microsoft.EntityFrameworkCore;
using CentralErros.Infrastructure.Mappings;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure
{
    public class CentralErrosContext : DbContext
    {
        public DbSet<ApplicationLayer> ApplicationLayers { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<User> Users { get; set; }

        // this constructor is for enable testing with in-memory data
        public CentralErrosContext(DbContextOptions<CentralErrosContext> options)
            : base(options)
        {
        }

        public CentralErrosContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CentralErros;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationLayerConfiguration());
            modelBuilder.ApplyConfiguration(new EnvironmentConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new LevelConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}