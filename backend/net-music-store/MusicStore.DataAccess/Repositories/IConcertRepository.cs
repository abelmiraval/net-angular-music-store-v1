using MusicStore.Entities;
using MusicStore.Entities.Complex;

namespace MusicStore.DataAccess.Repositories;

public interface IConcertRepository
{
    Task<(ICollection<ConcertInfo> Collection, int Total)> GetCollectionAsync(string? filter, int page, int rows, bool home = true);

    Task<ICollection<ConcertInfo>> GetCollectionByGenre(int id);

    Task<ICollection<ConcertMinimalInfo>> GetMinimalCollectionByGenre(int id);

    Task<Concert?> GetByIdAsync(int id);

    Task<int> CreateAsync(Concert concert);

    Task UpdateAsync();

    Task Finalize(int id);

    Task DeleteAsync(int id);
}
