using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Entities;
using System.Reflection.Emit;

namespace MusicStore.DataAccess.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {

            builder
                .Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasDefaultValue(true);

            builder.HasQueryFilter(p => p.Status);
        }
    }
}
