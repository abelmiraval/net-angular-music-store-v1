using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.DataAccess;
using MusicStore.Dto;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;

namespace MusicStore.API.Controllers
{
    [Route(Constants.DefaultRoute)]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public ConcertsController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<DtoResponseConcert>), 200)]
        public async Task<IActionResult> Get(string? filter)
        {
            //var response = new BaseResponseGeneric<ICollection<>>();

            // var list = await _context.Set<Concert>()
            //     //.Include(p => p.Genre) // Eager Loading
            //     .Where(p => p.Title.StartsWith(filter ?? string.Empty))
            //     .Select(p => new
            //     {
            //         p.Id,
            //         p.Title,
            //         p.Description,
            //         Genero = p.Genre.Description
            //     })
            //     .ToListAsync();

            var list = await _context.Set<Concert>()
                .Where(p => p.Title.Contains(filter ?? string.Empty))
                .ProjectTo<DtoResponseConcert>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<Genre>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<Genre>), 404)]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<Concert>();

            var concert = await _context.Set<Concert>()
                .FindAsync(id);
            //.FirstOrDefaultAsync(p => p.Id == id);

            if (concert == null)
                return NotFound(response);

            response.ResponseResult = concert;
            response.Success = true;

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 201)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 400)]
        public async Task<IActionResult> Post(DtoConcert request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                var concert = _mapper.Map<Concert>(request);

                await _context.Set<Concert>().AddAsync(concert);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.ResponseResult = concert.Id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return Created($"api/Concerts/{response.ResponseResult}", response);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 400)]
        public async Task<IActionResult> Put(int id, DtoConcert request)
        {
            var response = new BaseResponse();

            try
            {
                // var entity = await _context.Set<Concert>()
                //     .FindAsync(id);

                // if (entity != null)
                // {
                // entity.Description = request.Description ?? string.Empty;
                // entity.Title = request.Title;
                // entity.DateEvent = Convert.ToDateTime($"{request.DateEvent} {request.TimeEvent}");
                // entity.UnitPrice = request.UnitPrice;
                // entity.ImageUrl = request.ImageUrl;
                // entity.Place = request.Place;
                // entity.GenreId = request.GenreId;
                // entity.TicketsQuantity = request.TicketsQuantity;

                var entity = _mapper.Map<Concert>(request);
                entity.Id = id;
                entity.Status = true;

                _context.Attach(entity);

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                response.Success = true;
                // }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return Ok(response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 404)]
        public async Task<IActionResult> Patch(int id)
        {
            var response = new BaseResponse();
            var entity = await _context
                .Set<Concert>()
                .AsTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity != null)
            {
                entity.Finalized = true;

                await _context.SaveChangesAsync();

                response.Success = true;
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse();

            try
            {
                var entity = await _context.Set<Concert>()
                    .AsTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (entity != null)
                {
                    entity.Status = false;
                    await _context.SaveChangesAsync();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return Ok(response);
        }

    }
}
