using PrePerchaseServer.Models.hotel;
using PrePerchaseServer.Models.mediaBank;

namespace PrePerchaseServer.Models.hotel.repository
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAllAsync();
        Task<Hotel?> GetByIdAsync(Guid id);
        Task AddAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Mediabank>> GetMediaByIdsAsync(List<Guid> ids);
        Task<List<Mediabank>> GetMediaByHotelIdAsync(Guid hotelId);
        Task SaveChangesAsync();
    }
}