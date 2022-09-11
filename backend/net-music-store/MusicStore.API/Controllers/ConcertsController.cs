using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Entities;

namespace MusicStore.API.Controllers
{
    [Route(Constants.DefaultRoute)]
    [ApiController]
    public class ConcertsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hola Mundo");
        }
    }
}
