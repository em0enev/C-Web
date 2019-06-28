using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Panda.Domain;

namespace Panda.Data
{
    public class PandaDbContext : IdentityDbContext<PandaUser,PandaUserRole,string>
    {
        public DbSet<PandaUser> PandaUsers { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Package> Packages { get; set; }

        public PandaDbContext(DbContextOptions<PandaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PandaUser>()
                .HasKey(x => x.Id);

            builder.Entity<PandaUser>()
                .HasMany(x => x.Packages)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId);

            builder.Entity<PandaUser>()
                .HasMany(x => x.Receipts)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId);

            builder.Entity<Package>()
                .HasOne(x => x.Receipt)
                .WithOne(x => x.Package)
                .HasForeignKey<Receipt>(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
