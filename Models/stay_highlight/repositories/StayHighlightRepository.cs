using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;

namespace PrePerchaseServer.Models.stay_highlight.repositories
{
    public class StayHighlightRepository : IStayHighlightRepository
    {
        private readonly AppDbContext _context;
        public StayHighlightRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<StayHighlight>> GetAllAsync()
        {
            return await _context.StayHighlights.ToListAsync();
        }
        public async Task<StayHighlight?> GetByIdAsync(Guid id)
        {
            return await _context.StayHighlights
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<StayHighlight> CreateAsync(StayHighlight stayHighlight)
        {
            _context.StayHighlights.Add(stayHighlight);
            await _context.SaveChangesAsync();
            return stayHighlight;
        }
        public async Task<StayHighlight> UpdateAsync(StayHighlight stayHighlight)
        {
            _context.StayHighlights.Update(stayHighlight);
            await _context.SaveChangesAsync();
            return stayHighlight;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var stayHighlight = await _context.StayHighlights
                .FirstOrDefaultAsync(x => x.Id == id);
            if (stayHighlight == null)
                return false;
            _context.StayHighlights.Remove(stayHighlight);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}