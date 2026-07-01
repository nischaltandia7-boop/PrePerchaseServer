using PrePerchaseServer.Models.hotelgroup.dto;

namespace PrePerchaseServer.Models.hotelgroup.service;

    public interface IHotelGroupService
    {
        Task<List<HotelGroupResponseDto>> GetAllAsync();
        Task<HotelGroupResponseDto?> GetByIdAsync(Guid id);
        Task<HotelGroupResponseDto> CreateAsync(CreateHotelGroupDto dto);
        Task<HotelGroupResponseDto?> UpdateAsync(Guid id, UpdateHotelGroupDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
