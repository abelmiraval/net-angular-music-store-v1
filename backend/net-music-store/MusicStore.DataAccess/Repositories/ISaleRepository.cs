using MusicStore.Entities;
using MusicStore.Entities.Complex;

namespace MusicStore.DataAccess.Repositories;

public interface ISaleRepository
{
    Task<int> CreateAsync(Sale entity);

    Task<SaleInfo?> GetSaleById(int id);

    Task<ICollection<SaleInfo>> GetSaleCollection(int genreId, DateTime? dateInit, DateTime? dateEnd);

    Task<ICollection<SaleInfo>> GetSaleByUserId(string userId);

    Task<ICollection<ReportSaleInfo>> GetReportSale(int genreId, DateTime dateInit, DateTime dateEnd);
}