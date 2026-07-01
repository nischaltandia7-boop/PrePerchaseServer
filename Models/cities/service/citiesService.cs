using PrePerchaseServer.Models.cities;
using PrePerchaseServer.Models.cities.dto;
using PrePerchaseServer.Models.cities.repositories;

namespace PrePerchaseServer.Models.cities.service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _repo;

        public CityService(ICityRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CityResponseDto>> GetAllAsync()
        {
            var cities = await _repo.GetAllAsync();

            return cities.Select(c => new CityResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                State = c.State,
                Country = c.Country,
                Description = c.Description,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }

        public async Task<CityResponseDto?> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;

            return new CityResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                State = c.State,
                Country = c.Country,
                Description = c.Description,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            };
        }

        public async Task<CityResponseDto> CreateAsync(CreateCityDto dto)
        {
            var city = new City
            {
                Name = dto.Name,
                State = dto.State,
                Country = dto.Country,
                Description = dto.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(city);
            await _repo.SaveAsync();

            return await GetByIdAsync(city.Id)!;
        }

        public async Task<CityResponseDto?> UpdateAsync(int id, UpdateCityDto dto)
        {
            var city = await _repo.GetByIdAsync(id);
            if (city == null) return null;

            city.Name = dto.Name;
            city.State = dto.State;
            city.Country = dto.Country;
            city.Description = dto.Description;
            city.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(city);
            await _repo.SaveAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var city = await _repo.GetByIdAsync(id);
            if (city == null) return false;

            await _repo.DeleteAsync(city);
            await _repo.SaveAsync();

            return true;
        }
    }
}