using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Models.hotel.dto;
using PrePerchaseServer.Models.hotel.service;

namespace PrePerchaseServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _service;

        public HotelsController(IHotelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<HotelResponseDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<HotelResponseDto>> GetById(Guid id)
        {
            var hotel = await _service.GetByIdAsync(id);
            return hotel == null ? NotFound() : Ok(hotel);
        }

        [HttpPost]
        public async Task<ActionResult<HotelResponseDto>> Create(CreateHotelDto dto)
        {
            var hotel = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<HotelResponseDto>> Update(Guid id, UpdateHotelDto dto)
        {
            var hotel = await _service.UpdateAsync(id, dto);
            return hotel == null ? NotFound() : Ok(hotel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}