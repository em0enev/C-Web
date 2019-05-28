using IRunes.Models;

namespace IRunes.Data
{
    using Microsoft.EntityFrameworkCore;

    public class RunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public RunesDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseConfiguration.connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Track>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Album>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Album>()
                .Property(x => x.Cover);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Tracks)
                .WithOne(a => a.Album);
        }
    }
}
