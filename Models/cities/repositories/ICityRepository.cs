using PrePerchaseServer.Models.cities;

namespace PrePerchaseServer.Models.cities.repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllAsync();
        Task<City?> GetByIdAsync(int id);
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(City city);
        Task SaveAsync();
    }
}