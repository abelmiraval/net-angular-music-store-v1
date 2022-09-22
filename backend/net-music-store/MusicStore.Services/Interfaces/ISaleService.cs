using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities.Complex;

namespace MusicStore.Services.Interfaces;

public interface ISaleService
{
    Task<BaseResponseGeneric<int>> CreateAsync(DtoSale entity);

    Task<BaseResponseGeneric<DtoSaleInfo>> GetSaleById(int id);

    Task<BaseResponseGeneric<ICollection<DtoSaleInfo>>> GetSaleCollection(int genreId, string? dateInit, string? dateEnd);

    Task<BaseResponseGeneric<ICollection<DtoSaleInfo>>> GetSaleByUserId(string userId);

    Task<BaseResponseGeneric<ICollection<ReportSaleInfo>>> GetReportSale(int genreId, string dateInit, string dateEnd);
}