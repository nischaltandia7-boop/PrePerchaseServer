using PrePerchaseServer.Models.roomcategory;

namespace PrePerchaseServer.Models.roomcategory.repository
{
    public interface IRoomCategoryRepository
    {
        Task<List<RoomCategory>> GetAllAsync();
        Task<RoomCategory?> GetByIdAsync(Guid id);
        Task AddAsync(RoomCategory entity);
        Task UpdateAsync(RoomCategory entity);
        Task<bool> DeleteAsync(Guid id);
    }
}