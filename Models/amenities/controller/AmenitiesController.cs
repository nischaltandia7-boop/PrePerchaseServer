using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Models.amenities.service;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenitiesService _amenitiesService;

        public AmenitiesController(IAmenitiesService amenitiesService)
        {
            _amenitiesService = amenitiesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AmenitiesResponseDto>>> GetAll()
        {
            var amenities = await _amenitiesService.GetAllAsync();
            return Ok(amenities);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<AmenitiesResponseDto>> GetBySlug(string slug)
        {
            var amenity = await _amenitiesService.GetBySlugAsync(slug);

            if (amenity == null)
                return NotFound();

            return Ok(amenity);
        }

        [HttpPost]
        public async Task<ActionResult<AmenitiesResponseDto>> Create(CreateAmenitiesDto dto)
        {
            try
            {
                var created = await _amenitiesService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetBySlug),
                    new { slug = created.Slug },
                    created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{slug}")]
        public async Task<ActionResult<AmenitiesResponseDto>> Update(
            string slug,
            CreateAmenitiesDto dto)
        {
            var updated = await _amenitiesService.UpdateAsync(slug, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var deleted = await _amenitiesService.DeleteAsync(slug);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}