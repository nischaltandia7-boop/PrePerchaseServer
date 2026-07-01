using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;

namespace PrePerchaseServer.Models.amenities.repository
{
    public class AmenitiesRepository : IAmenitiesRepository
    {
        private readonly AppDbContext _context;

        public AmenitiesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Amenities>> GetAllAsync()
        {
            return await _context.Amenities
                .Include(a=>a.AmenitiesImages)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Amenities?> GetBySlugAsync(string slug)
        {
            return await _context.Amenities
                .Include(a=>a.AmenitiesImages)
                .FirstOrDefaultAsync(x => x.Slug == slug);
                
                
        }

        public async Task AddAsync(Amenities amenities)
        {
            await _context.Amenities.AddAsync(amenities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Amenities amenities)
        {
            _context.Amenities.Update(amenities);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(string slug)
        {
            var amenity = await _context.Amenities
                .FirstOrDefaultAsync(x => x.Slug == slug);

            if (amenity == null)
                return false;

            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}