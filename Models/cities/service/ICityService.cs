using PrePerchaseServer.Models.cities.dto;

namespace PrePerchaseServer.Models.cities.service
{
    public interface ICityService
    {
        Task<List<CityResponseDto>> GetAllAsync();
        Task<CityResponseDto?> GetByIdAsync(int id);
        Task<CityResponseDto> CreateAsync(CreateCityDto dto);
        Task<CityResponseDto?> UpdateAsync(int id, UpdateCityDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
