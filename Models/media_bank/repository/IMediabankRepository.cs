// Repositories/IMediabankRepository.cs
using PrePerchaseServer.Models.mediaBank;

public interface IMediabankRepository
{
    Task<Mediabank> CreateAsync(Mediabank media);
    Task<List<Mediabank>> GetAllAsync();
    Task<Mediabank?> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
}