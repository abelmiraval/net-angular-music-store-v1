using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Entities;

namespace MusicStore.DataAccess.Configurations;

public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
{
    public void Configure(EntityTypeBuilder<Concert> builder)
    {
        builder
            .Property(e => e.UnitPrice)
            .HasPrecision(5, 2);

        builder
            .Property(e => e.ImageUrl)
            .HasMaxLength(500)
            .IsUnicode(false);

        builder.Property(p => p.Status)
            .HasDefaultValue(true);

        builder.HasQueryFilter(p => p.Status);

    }
}
