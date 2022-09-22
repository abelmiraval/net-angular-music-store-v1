using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Entities.Complex;

namespace MusicStore.DataAccess.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly MusicStoreDbContext _context;
    private readonly IMapper _mapper;

    public SaleRepository(MusicStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(Sale entity)
    {
        var number = await _context.Set<Sale>()
            .IgnoreQueryFilters()
            .CountAsync();

        number++;

        entity.OperationNumber = $"{number:00000}";

        await _context.Set<Sale>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<SaleInfo?> GetSaleById(int id)
    {
        return await _context.Set<Sale>()
            .Include(p => p.Concert)
            .ThenInclude(p => p.Genre)
            .Where(p => p.Id == id)
            .Select(p => _mapper.Map<SaleInfo>(p))
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<SaleInfo>> GetSaleCollection(int genreId, DateTime? dateInit, DateTime? dateEnd)
    {
        var query = _context.Set<Sale>()
            .Include(p => p.Concert)
            .ThenInclude(p => p.Genre)
            .AsQueryable();

        // 2022-01-01 00:00:00 Inicio
        // 2022-04-28 00:00:00 Fin

        if (dateInit != null && dateEnd != null)
        {
            query = query.Where(p => dateInit <= p.SaleDate && dateEnd >= p.SaleDate);
        }

        query = query.Where(p => p.Concert.GenreId == genreId);

        var collection = await query
            .Select(p => _mapper.Map<SaleInfo>(p))
            .ToListAsync();

        return collection;

    }

    public async Task<ICollection<SaleInfo>> GetSaleByUserId(string userId)
    {
        return await _context.Set<Sale>()
            .Include(p => p.Concert)
            .ThenInclude(p=> p.Genre)
            .Where(x => x.UserId == userId)
            .Select(p => _mapper.Map<SaleInfo>(p))
            .ToListAsync();
    }

    public async Task<ICollection<ReportSaleInfo>> GetReportSale(int genreId, DateTime dateInit, DateTime dateEnd)
    {
        return await _context.Set<Sale>()
            .Include(p => p.Concert)
            .Where(p => p.Concert.GenreId == genreId)
            .GroupBy(x => x.Concert.Title)
            .Select(p => new ReportSaleInfo
            {
                ConcertName = p.Key,
                TotalSale = p.Sum(x => x.TotalSale)
            })
            .ToListAsync();
    }
}