using MediatR;
using PrePerchaseServer.Models.amenities;
using PrePerchaseServer.Models.amenities.repository;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Commands.CreateAmenity;
public class CreateAmenityCommandHandler
    : IRequestHandler<CreateAmenityCommand, AmenitiesResponseDto>
{
    private readonly IAmenitiesRepository _repo;

    public CreateAmenityCommandHandler(IAmenitiesRepository repo)
    {
        _repo = repo;
    }

    public async Task<AmenitiesResponseDto> Handle(
        CreateAmenityCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await _repo.GetBySlugAsync(request.Dto.Slug);

        if (existing != null)
            throw new Exception("Slug already exists");

        var amenity = new Amenities
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            Slug = request.Dto.Slug,
            Type = request.Dto.Type,
            IconId = request.Dto.IconId,
            Status = request.Dto.Status,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(amenity);

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