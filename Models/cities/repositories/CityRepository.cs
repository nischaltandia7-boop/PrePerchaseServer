using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;
using PrePerchaseServer.Models.cities;

namespace PrePerchaseServer.Models.cities.repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task AddAsync(City city)
        {
            await _context.Cities.AddAsync(city);
        }

        public async Task UpdateAsync(City city)
        {
            _context.Cities.Update(city);
        }

        public async Task DeleteAsync(City city)
        {
            _context.Cities.Remove(city);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}