using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Services.Interfaces;

public interface IGenreService
{
    Task<BaseResponseGeneric<ICollection<DtoResponseGenre>>> ListAsync(string? filter);

    Task<BaseResponseGeneric<DtoResponseGenre?>> GetByIdAsync(int id);

    Task<BaseResponseGeneric<int>> CreateAsync(DtoGenre request);

    Task<BaseResponse> UpdateAsync(int id, DtoGenre request);

    Task<BaseResponse> DeleteAsync(int id);
}