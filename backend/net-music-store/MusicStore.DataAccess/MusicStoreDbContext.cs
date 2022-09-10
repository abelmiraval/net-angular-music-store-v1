using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;

namespace MusicStore.DataAccess
{
    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext()
        {

        }

        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Concert>()
                .Property(e => e.UnitPrice)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Concert>()
                .Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
        }

        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Genre> Genres { get; set; }


    }
}
