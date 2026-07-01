using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Models.cities.dto;
using PrePerchaseServer.Models.cities.service;

namespace PrePerchaseServer.Models.cities
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityResponseDto>>> GetAll()
        {
            var cities = await _cityService.GetAllAsync();
            return Ok(cities);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CityResponseDto>> GetById(int id)
        {
            var city = await _cityService.GetByIdAsync(id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<CityResponseDto>> Create(CreateCityDto dto)
        {
            var createdCity = await _cityService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdCity.Id }, createdCity);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CityResponseDto>> Update(int id, UpdateCityDto dto)
        {
            var updatedCity = await _cityService.UpdateAsync(id, dto);

            if (updatedCity == null)
                return NotFound();

            return Ok(updatedCity);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cityService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}