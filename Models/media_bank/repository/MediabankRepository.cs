// Repositories/MediabankRepository.cs
using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;
using PrePerchaseServer.Models.mediaBank;

public class MediabankRepository : IMediabankRepository
{
    private readonly AppDbContext _db;

    public MediabankRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Mediabank> CreateAsync(Mediabank media)
    {
        _db.Mediabank.Add(media);
        await _db.SaveChangesAsync();
        return media;
    }

    public async Task<List<Mediabank>> GetAllAsync()
    {
        return await _db.Mediabank
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Mediabank?> GetByIdAsync(Guid id)
    {
        return await _db.Mediabank.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var data = await _db.Mediabank.FindAsync(id);
        if (data != null)
        {
            _db.Mediabank.Remove(data);
            await _db.SaveChangesAsync();
        }
    }
}