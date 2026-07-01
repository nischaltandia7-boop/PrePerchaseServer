using PrePerchaseServer.Models.amenities.repository;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.service
{
    public class AmenitiesService : IAmenitiesService
    {
        private readonly IAmenitiesRepository _repo;

        public AmenitiesService(IAmenitiesRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AmenitiesResponseDto>> GetAllAsync()
        {
            var amenities = await _repo.GetAllAsync();

            return amenities
                .Select(MapToResponse)
                .ToList();
        }

        public async Task<AmenitiesResponseDto?> GetBySlugAsync(string slug)
        {
            var amenity = await _repo.GetBySlugAsync(slug);

            if (amenity == null)
                return null;

            return MapToResponse(amenity);
        }

        public async Task<AmenitiesResponseDto> CreateAsync(CreateAmenitiesDto dto)
        {
            var existing = await _repo.GetBySlugAsync(dto.Slug);

            if (existing != null)
                throw new Exception("Slug already exists");

            var amenity = new Amenities
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Slug = dto.Slug,
                Type = dto.Type,
                IconId = dto.IconId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(amenity);

            return MapToResponse(amenity);
        }

        public async Task<AmenitiesResponseDto?> UpdateAsync(string slug, CreateAmenitiesDto dto)
        {
            var amenity = await _repo.GetBySlugAsync(slug);

            if (amenity == null)
                return null;

            amenity.Name = dto.Name;
            amenity.Slug = dto.Slug;
            amenity.Type = dto.Type;
            amenity.IconId = dto.IconId;
            amenity.Status = dto.Status;
            amenity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(amenity);

            return MapToResponse(amenity);
        }

        public async Task<bool> DeleteAsync(string slug)
        {
            return await _repo.DeleteAsync(slug);
        }

        private static AmenitiesResponseDto MapToResponse(Amenities amenity)
        {
            return new AmenitiesResponseDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Slug = amenity.Slug,
                Type = amenity.Type,
                IconId = amenity.IconId,
                Status = amenity.Status,
                CreatedAt = amenity.CreatedAt,
                UpdatedAt = amenity.UpdatedAt,
            };
        }
    }
}