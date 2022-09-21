using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.DataAccess.Repositories;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Complex;
using MusicStore.Services.Interfaces;

namespace MusicStore.Services.Implementations;

public class ConcertService : IConcertService
{
    private readonly IConcertRepository _repository;
    private readonly IMapper _mapper;
    private readonly IFileUploader _fileUploader;
    private readonly ILogger<ConcertService> _logger;

    public ConcertService(IConcertRepository repository, IMapper mapper, IFileUploader fileUploader, ILogger<ConcertService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _fileUploader = fileUploader;
        _logger = logger;
    }

    public async Task<BaseCollectionResponse<ICollection<ConcertInfo>>> GetAsync(string? filter, int page, int rows,
        bool fromHome = true)
    {
        var response = new BaseCollectionResponse<ICollection<ConcertInfo>>();
        try
        {
            var tuple = await _repository.GetCollectionAsync(filter, page, rows, fromHome);
            response.ResponseResult = tuple.Collection.ToList();
            var totalPages = tuple.Total / rows;
            if (tuple.Total % rows > 0)
                totalPages++;

            response.TotalPages = totalPages;
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.StackTrace);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<ConcertInfo>>> GetByGenreAsync(int genreId)
    {
        var response = new BaseResponseGeneric<ICollection<ConcertInfo>>();
        try
        {
            response.ResponseResult = await _repository.GetCollectionByGenre(genreId);
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.StackTrace);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<ConcertMinimalInfo>>> GetMinimalByGenreAsync(int genreId)
    {
        var response = new BaseResponseGeneric<ICollection<ConcertMinimalInfo>>();
        try
        {
            response.ResponseResult = await _repository.GetMinimalCollectionByGenre(genreId);
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.StackTrace);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<Concert>> GetAsync(int id)
    {
        var response = new BaseResponseGeneric<Concert>();
        try
        {
            response.ResponseResult = await _repository.GetByIdAsync(id) ?? new Concert();
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.StackTrace);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<int>> CreateAsync(DtoConcert request)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
            var concert = _mapper.Map<Concert>(request);

            if (!string.IsNullOrEmpty(request.Filename))
            {
                concert.ImageUrl = await _fileUploader.UploadFileAsync(request.ImageBase64, request.Filename);
            }

            response.ResponseResult = await _repository.CreateAsync(concert);

            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.StackTrace);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> FinalizeAsync(int id)
    {
        var response = new BaseResponse();

        try
        {
            await _repository.Finalize(id);

            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, DtoConcert request)
    {
        var response = new BaseResponse();
        try
        {
            var concert = await _repository.GetByIdAsync(id);

            if (concert == null)
            {
                response.Success = false;
                return response;
            }

            _mapper.Map(request, concert);

            if (!string.IsNullOrEmpty(request.Filename))
            {
                concert.ImageUrl = await _fileUploader.UploadFileAsync(request.ImageBase64, request.Filename);
            }

            await _repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.StackTrace);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
            await _repository.DeleteAsync(id);

            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.StackTrace);
            response.Success = false;
            response.ListErrors.Add(ex.Message);
        }

        return response;
    }
}
