using PrePerchaseServer.Models.stay_highlight.dto;

namespace PrePerchaseServer.Models.stay_highlight.service
{
    public interface IStayHighlightService
    {
        Task<List<StayHighlightResponseDto>> GetAllAsync();
        Task<StayHighlightResponseDto?> GetByIdAsync(Guid id);
        Task<StayHighlightResponseDto> CreateAsync(CreateStayHighlightDto dto);
        Task<StayHighlightResponseDto?> UpdateAsync(Guid id, UpdateStayHighlightDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}