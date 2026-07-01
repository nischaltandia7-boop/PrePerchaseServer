
using PrePerchaseServer.Models.mediaBank;
namespace PrePerchaseServer.Models.mediaBank;
public interface IMediabankService
{
    Task<Mediabank> UploadAsync(IFormFile file, CreateMediabankDto dto);
    Task<List<Mediabank>> GetAllAsync();
    Task DeleteAsync(Guid id);
}