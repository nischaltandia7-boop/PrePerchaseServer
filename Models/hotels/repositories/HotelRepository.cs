using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;
using PrePerchaseServer.Models.mediaBank;

namespace PrePerchaseServer.Models.hotel.repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;

        public HotelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Hotel>> GetAllAsync()
        {
            return await _context.Hotel
                .Include(h => h.RoomCategories)
                    .ThenInclude(rc => rc.RoomImg)   // 👈 IMPORTANT FIX
                .Include(h => h.CancellationPolicies)
                    .ThenInclude(p => p.Slabs)
                .Include(h => h.HotelImages)
                .ToListAsync();
        }

        public async Task<Hotel?> GetByIdAsync(Guid id)
        {
            return await _context.Hotel
                .Include(h => h.RoomCategories)
                    .ThenInclude(rc => rc.RoomImg)   // 👈 IMPORTANT FIX
                .Include(h => h.CancellationPolicies)
                    .ThenInclude(p => p.Slabs)
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddAsync(Hotel hotel)
        {
            await _context.Hotel.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            _context.Hotel.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null) return false;

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }

        // Media helpers
        public async Task<List<Mediabank>> GetMediaByIdsAsync(List<Guid> ids)
        {
            return await _context.Mediabank
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<List<Mediabank>> GetMediaByHotelIdAsync(Guid hotelId)
        {
            return await _context.Mediabank
                .Where(x => x.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}