using PrePerchaseServer.Models.roomcategory.dto;

namespace PrePerchaseServer.Models.room_category.service
{
    public interface IRoomCategoryService
    {
        Task<List<RoomCategoryResponseDto>> GetAllAsync();
        Task<RoomCategoryResponseDto?> GetByIdAsync(Guid id);
        Task<RoomCategoryResponseDto> CreateAsync(CreateRoomCategoryDto dto);
        Task<RoomCategoryResponseDto?> UpdateAsync(Guid id, UpdateRoomCategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}