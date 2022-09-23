using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Complex;

namespace MusicStore.Services.Interfaces;

public interface IConcertService
{
    Task<BaseCollectionResponse<ICollection<ConcertInfo>>> GetAsync(string? filter, int page, int rows, bool fromHome = true);

    Task<BaseResponseGeneric<ICollection<ConcertInfo>>> GetByGenreAsync(int genreId);

    Task<BaseResponseGeneric<ICollection<ConcertMinimalInfo>>> GetMinimalByGenreAsync(int genreId);

    Task<BaseResponseGeneric<Concert>> GetAsync(int id);

    Task<BaseResponseGeneric<int>> CreateAsync(DtoConcert request);

    Task<BaseResponse> FinalizeAsync(int id);

    Task<BaseResponse> UpdateAsync(int id, DtoConcert request);

    Task<BaseResponse> DeleteAsync(int id);
}
