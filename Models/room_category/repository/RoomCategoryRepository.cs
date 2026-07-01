using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;

namespace PrePerchaseServer.Models.roomcategory.repository
{
    public class RoomCategoryRepository : IRoomCategoryRepository
    {
        private readonly AppDbContext _context;

        public RoomCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomCategory>> GetAllAsync()
        {
            return await _context.RoomCategories
                .Include(r=>r.RoomImg)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RoomCategory?> GetByIdAsync(Guid id)
        {
            return await _context.RoomCategories
                .Include(r=>r.RoomImg)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(RoomCategory entity)
        {
            await _context.RoomCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomCategory entity)
        {
            _context.RoomCategories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.RoomCategories.FindAsync(id);
            if (item == null) return false;

            _context.RoomCategories.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}