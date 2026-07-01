using PrePerchaseServer.Models.stay_highlight.dto;
using PrePerchaseServer.Models.stay_highlight.repositories;

namespace PrePerchaseServer.Models.stay_highlight.service
{
    public class StayHighlightService : IStayHighlightService
    {
        private readonly IStayHighlightRepository _repository;

        public StayHighlightService(IStayHighlightRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StayHighlightResponseDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(x => new StayHighlightResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
        }

        public async Task<StayHighlightResponseDto?> GetByIdAsync(Guid id)
        {
            var x = await _repository.GetByIdAsync(id);

            if (x == null) return null;

            return new StayHighlightResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            };
        }

        public async Task<StayHighlightResponseDto> CreateAsync(CreateStayHighlightDto dto)
        {
            var entity = new StayHighlight
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Slug = GenerateSlug(dto.Name),
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(entity);

            return new StayHighlightResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<StayHighlightResponseDto?> UpdateAsync(Guid id, UpdateStayHighlightDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return null;

            if (!string.IsNullOrEmpty(dto.Name))
            {
                entity.Name = dto.Name;
                entity.Slug = GenerateSlug(dto.Name);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(entity);

            return new StayHighlightResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        // 🔥 helper method
        private string GenerateSlug(string name)
        {
            return name
                .ToLower()
                .Replace(" ", "-")
                .Trim();
        }
    }
}