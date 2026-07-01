namespace PrePerchaseServer.Models.stay_highlight.repositories
{
    public interface IStayHighlightRepository
    {
        Task<List<StayHighlight>> GetAllAsync();
        Task<StayHighlight?> GetByIdAsync(Guid id);
        Task<StayHighlight> CreateAsync(StayHighlight stayHighlight);
        Task<StayHighlight> UpdateAsync(StayHighlight stayHighlight);
        Task<bool> DeleteAsync(Guid id);
    }
}