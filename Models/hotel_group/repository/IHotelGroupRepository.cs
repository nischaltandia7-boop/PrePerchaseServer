using PrePerchaseServer.Models.hotelgroup;

namespace PrePerchaseServer.Models.hotelgroup.repository
{
    public interface IHotelGroupRepository
    {
        Task<List<HotelGroup>> GetAllAsync();
        Task<HotelGroup?> GetByIdAsync(Guid id);
        Task AddAsync(HotelGroup hotelGroup);
        Task UpdateAsync(HotelGroup hotelGroup);
        Task<bool> DeleteAsync(Guid id);
    }
}