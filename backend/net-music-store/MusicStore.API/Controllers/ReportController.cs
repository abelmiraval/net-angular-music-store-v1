using Microsoft.AspNetCore.Mvc;
using MusicStore.Entities;
using MusicStore.Services.Interfaces;

namespace MusicStore.API.Controllers;

[ApiController]
[Route($"{Constants.DefaultRoute}/[action]")]
public class ReportController : Controller
{
    private readonly ISaleService _service;

    public ReportController(ISaleService service)
    {
        _service = service;
    }

    //GET
    [HttpGet]
    public async Task<IActionResult> Sales(int genreId, string dateInit, string dateEnd)
    {
        return Ok(await _service.GetReportSale(genreId, dateInit, dateEnd));
    }
}