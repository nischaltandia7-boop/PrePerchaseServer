using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Models.hotelgroup.dto;
using PrePerchaseServer.Models.hotelgroup.service;

namespace PrePerchaseServer.Models.hotelgroup;

    [ApiController]
    [Route("api/[controller]")]
    public class HotelGroupsController : ControllerBase
    {
        private readonly IHotelGroupService _service;

        public HotelGroupsController(IHotelGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<HotelGroupResponseDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<HotelGroupResponseDto>> GetById(Guid id)
        {
            var hotelGroup = await _service.GetByIdAsync(id);

            if (hotelGroup == null)
                return NotFound();

            return Ok(hotelGroup);
        }

        [HttpPost]
        public async Task<ActionResult<HotelGroupResponseDto>> Create([FromBody] CreateHotelGroupDto dto)
        {
            var hotelGroup = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = hotelGroup.Id },
                hotelGroup);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<HotelGroupResponseDto>> Update(
            Guid id,
            [FromBody] UpdateHotelGroupDto dto)
        {
            var hotelGroup = await _service.UpdateAsync(id, dto);

            if (hotelGroup == null)
                return NotFound();

            return Ok(hotelGroup);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
