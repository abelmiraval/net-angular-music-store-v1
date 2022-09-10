using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MusicStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hola Mundo");
        }
    }
}
