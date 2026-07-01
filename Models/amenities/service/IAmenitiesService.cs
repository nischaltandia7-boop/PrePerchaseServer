using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.service
{
    public interface IAmenitiesService
    {
        Task<List<AmenitiesResponseDto>> GetAllAsync();
        Task<AmenitiesResponseDto?> GetBySlugAsync(string slug);
        Task<AmenitiesResponseDto> CreateAsync(CreateAmenitiesDto dto);
        Task<AmenitiesResponseDto?> UpdateAsync(string slug, CreateAmenitiesDto dto);
        Task<bool> DeleteAsync(string slug);
    }
}