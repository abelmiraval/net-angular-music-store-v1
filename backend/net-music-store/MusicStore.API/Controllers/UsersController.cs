using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Services.Interfaces;

namespace MusicStore.API.Controllers;

[ApiController]
[Route($"{Constants.DefaultRoute}/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(BaseResponseGeneric<string>), 200)]
    [ProducesResponseType(typeof(BaseResponseGeneric<string>), 400)]
    public async Task<IActionResult> Register(DtoRegisterUser request)
    {
        var response = await _service.RegisterAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }


    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(DtoLoginResponse), 200)]
    [ProducesResponseType(typeof(DtoLoginResponse), 401)]
    public async Task<IActionResult> Login(DtoLogin request)
    {
        var response = await _service.LoginAsync(request);

        return response.Success ? Ok(response) : Unauthorized(response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SendTokenToResetPassword([FromBody] DtoResetPassword request)
    {
        return Ok(await _service.SendTokenToResetPasswordAsync(request));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(DtoConfirmReset request)
    {
        return Ok(await _service.ResetPassword(request));
    }
}