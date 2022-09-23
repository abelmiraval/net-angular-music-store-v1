using System.Globalization;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.DataAccess.Repositories;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Complex;
using MusicStore.Services.Interfaces;

namespace MusicStore.Services.Implementations;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<SaleService> _logger;

    public SaleService(ISaleRepository repository, IMapper mapper, ILogger<SaleService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseGeneric<int>> CreateAsync(DtoSale request, string userId)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
            var sale = _mapper.Map<Sale>(request);
            sale.SaleDate = DateTime.Now;
            sale.TotalSale = request.Quantity * request.UnitPrice;
            sale.UserId = userId;

            response.ResponseResult = await _repository.CreateAsync(sale);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ListErrors.Add(ex.Message);
            response.Success = false;
            _logger.LogCritical(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<DtoSaleInfo>> GetSaleById(int id)
    {
        var response = new BaseResponseGeneric<DtoSaleInfo>();
        try
        {
            response.ResponseResult = _mapper.Map<DtoSaleInfo>(await _repository.GetSaleById(id));

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ListErrors.Add(ex.Message);
            response.Success = false;
            _logger.LogCritical(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<DtoSaleInfo>>> GetSaleCollection(int genreId, string? dateInit, string? dateEnd)
    {
        var response = new BaseResponseGeneric<ICollection<DtoSaleInfo>>();

        try
        {
            var englishFormat = new CultureInfo("en-US");

            var collection = await _repository.GetSaleCollection(genreId, 
                string.IsNullOrEmpty(dateInit) ? null : Convert.ToDateTime(dateInit, englishFormat),
                string.IsNullOrEmpty(dateEnd) ? null : Convert.ToDateTime(dateEnd, englishFormat));

            response.ResponseResult = collection
                .Select(p => _mapper.Map<DtoSaleInfo>(p))
                .ToList();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ListErrors.Add(ex.Message);
            response.Success = false;
            _logger.LogCritical(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<DtoSaleInfo>>> GetSaleByUserId(string userId)
    {
        var response = new BaseResponseGeneric<ICollection<DtoSaleInfo>>();

        try
        {
            var collection = await _repository.GetSaleByUserId(userId);

            response.ResponseResult = collection
                .Select(p => _mapper.Map<DtoSaleInfo>(p))
                .ToList();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ListErrors.Add(ex.Message);
            response.Success = false;
            _logger.LogCritical(ex.Message);
        }
        
        return response;
    }

    public Task<BaseResponseGeneric<ICollection<ReportSaleInfo>>> GetReportSale(int genreId, string? dateInit, string? dateEnd)
    {
        throw new NotImplementedException();
    }
}