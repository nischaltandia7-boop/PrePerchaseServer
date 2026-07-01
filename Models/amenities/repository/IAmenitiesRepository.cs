using PrePerchaseServer.Models.amenities;

namespace PrePerchaseServer.Models.amenities.repository
{
    public interface IAmenitiesRepository
    {
        Task<List<Amenities>> GetAllAsync();
        Task<Amenities?> GetBySlugAsync(string slug);
        Task AddAsync(Amenities amenity);
        Task UpdateAsync(Amenities amenity);
        Task<bool> DeleteAsync(string slug);
    }
}