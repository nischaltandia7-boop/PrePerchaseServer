using PrePerchaseServer.Models.hotel.dto;

namespace PrePerchaseServer.Models.hotel.service
{
    public interface IHotelService
    {
        Task<List<HotelResponseDto>> GetAllAsync();
        Task<HotelResponseDto?> GetByIdAsync(Guid id);
        Task<HotelResponseDto> CreateAsync(CreateHotelDto dto);
        Task<HotelResponseDto?> UpdateAsync(Guid id, UpdateHotelDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}