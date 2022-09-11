using System.Reflection;
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

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Genre>()
                .HasData(new List<Genre>()
                {
                    new Genre { Id = 1, Description = "Rock"},
                    new Genre { Id = 2, Description = "Salsa"},
                    new Genre { Id = 3, Description = "Reggeaton"}
                });
        }

    }
}
