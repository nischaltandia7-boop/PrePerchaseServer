using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;
using PrePerchaseServer.Models.hotelgroup;

namespace PrePerchaseServer.Models.hotelgroup.repository
{
    public class HotelGroupRepository : IHotelGroupRepository
    {
        private readonly AppDbContext _context;

        public HotelGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HotelGroup>> GetAllAsync()
        {
            return await _context.HotelGroups
                .Include(hg=>hg.HotelGroupImg)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<HotelGroup?> GetByIdAsync(Guid id)
        {
            return await _context.HotelGroups
                .Include(hg=>hg.HotelGroupImg)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(HotelGroup hotelGroup)
        {
            await _context.HotelGroups.AddAsync(hotelGroup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HotelGroup hotelGroup)
        {
            _context.HotelGroups.Update(hotelGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var hotelGroup = await _context.HotelGroups.FindAsync(id);

            if (hotelGroup == null)
                return false;

            _context.HotelGroups.Remove(hotelGroup);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}