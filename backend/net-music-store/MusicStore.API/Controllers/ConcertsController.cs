using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Services.Interfaces;

namespace MusicStore.API.Controllers
{
    [Route(Constants.DefaultRoute)]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService _service;

        public ConcertsController(IConcertService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<DtoResponseConcert>), 200)]
        public async Task<IActionResult> Get(string? filter, int page = 1, int rows = 10)
        {
            return Ok(await _service.GetAsync(filter, page, rows, false));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<Concert>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<Concert>), 404)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 201)]
        [ProducesResponseType(typeof(BaseResponseGeneric<int>), 400)]
        public async Task<IActionResult> Post(DtoConcert request)
        {
            var response = await _service.CreateAsync(request);

            return Created($"api/Concerts/{response.ResponseResult}", response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Put(int id, DtoConcert request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Patch(int id)
        {
            var response = await _service.FinalizeAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

    }
}
