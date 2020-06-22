using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CentralErros.Models
{
    public class CentralErrosContext : DbContext
    {
        public DbSet<ApplicationLayer> ApplicationLayers { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<Error> Errors { get; set; }
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
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Codenation;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ErrorConfiguration());
        }
    }
}