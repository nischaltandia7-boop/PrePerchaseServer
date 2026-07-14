using MediatR;
using PrePerchaseServer.Models.amenities.repository;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Commands.UpdateAmenity
{
    public class UpdateAmenityCommandHandler
        : IRequestHandler<UpdateAmenityCommand, AmenitiesResponseDto?>
    {
        private readonly IAmenitiesRepository _repo;

        public UpdateAmenityCommandHandler(IAmenitiesRepository repo)
        {
            _repo = repo;
        }

        public async Task<AmenitiesResponseDto?> Handle(
            UpdateAmenityCommand request,
            CancellationToken cancellationToken)
        {
            var amenity = await _repo.GetBySlugAsync(request.Slug);

            if (amenity == null)
                return null;

            amenity.Name = request.Dto.Name;
            amenity.Slug = request.Dto.Slug;
            amenity.Type = request.Dto.Type;
            amenity.IconId = request.Dto.IconId;
            amenity.Status = request.Dto.Status;
            amenity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(amenity);

            return new AmenitiesResponseDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Slug = amenity.Slug,
                Type = amenity.Type,
                IconId = amenity.IconId,
                Status = amenity.Status,
                CreatedAt = amenity.CreatedAt,
                UpdatedAt = amenity.UpdatedAt
            };
        }
    }
}