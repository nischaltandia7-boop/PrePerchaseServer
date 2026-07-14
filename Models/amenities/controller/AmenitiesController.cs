using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Models.amenities.Commands.CreateAmenity;
using PrePerchaseServer.Models.amenities.Commands.DeleteAmenity;
using PrePerchaseServer.Models.amenities.Commands.UpdateAmenity;
using PrePerchaseServer.Models.amenities.Queries.GetAmenities;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AmenitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<AmenitiesResponseDto>>> GetAll()
        {
            var amenities = await _mediator.Send(new GetAmenitiesQuery());

            return Ok(amenities);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<AmenitiesResponseDto>> GetBySlug(string slug)
        {
            var amenity = await _mediator.Send(new GetAmenityBySlugQuery(slug));

            if (amenity == null)
                return NotFound();

            return Ok(amenity);
        }

        [HttpPost]
        public async Task<ActionResult<AmenitiesResponseDto>> Create(CreateAmenitiesDto dto)
        {
            try
            {
                var created = await _mediator.Send(new CreateAmenityCommand(dto));

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
            var updated = await _mediator.Send(
                new UpdateAmenityCommand(slug, dto));

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            var deleted = await _mediator.Send(
                new DeleteAmenityCommand(slug));

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}