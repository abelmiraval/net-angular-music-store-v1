using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;

namespace MusicStore.DataAccess.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly MusicStoreDbContext _context;

    public GenreRepository(MusicStoreDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Genre>> ListAsync(string? filter)
    {
        var list = await _context.Set<Genre>()
                .Where(p => p.Description.StartsWith(filter ?? string.Empty))
                .ToListAsync();

        return list;
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        var genre = await _context.Set<Genre>()
                .AsTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

        return genre;
    }

    public async Task<int> CreateAsync(Genre entity)
    {
        await _context.Set<Genre>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await GetByIdAsync(id);
        if (genre == null) return;
        genre.Status = false;
        await _context.SaveChangesAsync();
    }
}
