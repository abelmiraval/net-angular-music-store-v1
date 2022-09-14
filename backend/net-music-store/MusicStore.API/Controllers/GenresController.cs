using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.DataAccess;
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

        [HttpGet()]
        public async Task<IActionResult> Get(string? filter)
        {
         return Ok(await _context.Set<Genre>()
                // .IgnoreQueryFilters()  //IGNORAR LOS QUERY FILTERS
                // .AsNoTracking()  //NO UTILIZAR EL CACHE
                .Where(p => p.Description.StartsWith(filter ?? string.Empty))
                .Select(p => new
                {
                    p.Id,
                    p.Description
                })
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var genre = await _context.Set<Genre>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }
    }
}
