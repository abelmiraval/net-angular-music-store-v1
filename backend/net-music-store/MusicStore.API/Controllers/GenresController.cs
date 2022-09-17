using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.DataAccess;
using MusicStore.Dto;
using MusicStore.Dto.Request;
using MusicStore.Entities;

namespace MusicStore.API.Controllers
{
    [ApiController]
    [Route(Constants.DefaultRoute)]
    public class GenresController : ControllerBase
    {
        private readonly MusicStoreDbContext _context;

        public GenresController(MusicStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseGeneric<Genre>), 200)]
        public async Task<IActionResult> Get(string? filter)
        {
            //var response = new BaseResponseGeneric<ICollection<>>();

            var list = await _context.Set<Genre>()
                   // .IgnoreQueryFilters()  //IGNORAR LOS QUERY FILTERS
                   // .AsNoTracking()  //NO UTILIZAR EL CACHE
                   .Where(p => p.Description.StartsWith(filter ?? string.Empty))
                   .Select(p => new
                   {
                       p.Id,
                       p.Description
                   })
                   .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<Genre>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<Genre>), 404)]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<Genre>();

            var genre = await _context.Set<Genre>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (genre == null)
                return NotFound(response);

            response.ResponseResult = genre;
            response.Success = true;

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 201)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 400)]
        public async Task<IActionResult> Post(DtoGenre request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                var genre = new Genre
                {
                    Description = request.Description ?? string.Empty
                };

                await _context.Set<Genre>().AddAsync(genre);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.ResponseResult = genre.Id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return Created($"api/Genres/{response.ResponseResult}", response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 400)]
        public async Task<IActionResult> Put(int id, DtoGenre request)
        {
            var response = new BaseResponse();

            try
            {
                var entity = await _context.Set<Genre>()
                    .AsTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (entity != null)
                {
                    entity.Description = request.Description ?? string.Empty;
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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse();

            try
            {
                var entity = await _context.Set<Genre>()
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
