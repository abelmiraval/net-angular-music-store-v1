using Microsoft.AspNetCore.Mvc;
using MusicStore.Entities;
using MusicStore.Services.Interfaces;

namespace MusicStore.API.Controllers;

[ApiController]
[Route(Constants.DefaultRoute)]
public class HomeController : ControllerBase
{
    private readonly IConcertService _concertService;
    private readonly IGenreService _genreService;

    public HomeController(IConcertService concertService, IGenreService genreService)
    {
        _concertService = concertService;
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var genres = await _genreService.ListAsync(null);

        var concerts = await _concertService.GetAsync(null, 1, 100, true);
        var response = new
        {
            Genres = genres.Result,
            Concerts = concerts.Result,
            Success = true
        };

        return Ok(response);
    }
}