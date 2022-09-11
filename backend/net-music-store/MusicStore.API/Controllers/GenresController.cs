using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Set<Genre>()
                .Where(p => p.Description.StartsWith("R"))
                .Select(p => new
                {
                    p.Id,
                    p.Description
                })
                .ToList());
        }
    }
}
