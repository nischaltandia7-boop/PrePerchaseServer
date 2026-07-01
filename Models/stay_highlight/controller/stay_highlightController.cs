using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Models.stay_highlight.dto;
using PrePerchaseServer.Models.stay_highlight.service;

namespace PrePerchaseServer.Models.stay_highlight
{
    [ApiController]
    [Route("api/[controller]")]
    public class StayHighlightsController : ControllerBase
    {
        private readonly IStayHighlightService _stayHighlightService;
        public StayHighlightsController(IStayHighlightService stayHighlightService)
        {
            _stayHighlightService = stayHighlightService;
        }

        // GET: api/stayhighlights
        [HttpGet]
        public async Task<ActionResult<List<StayHighlightResponseDto>>> GetAll()
        {
            var result = await _stayHighlightService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/stayhighlights/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<StayHighlightResponseDto>> GetById(Guid id)
        {
            var result = await _stayHighlightService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/stayhighlights
        [HttpPost]
        public async Task<ActionResult<StayHighlightResponseDto>> Create(CreateStayHighlightDto dto)
        {
            var created = await _stayHighlightService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }

        // PUT: api/stayhighlights/{id}
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<StayHighlightResponseDto>> Update(Guid id, UpdateStayHighlightDto dto)
        {
            var updated = await _stayHighlightService.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/stayhighlights/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _stayHighlightService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}