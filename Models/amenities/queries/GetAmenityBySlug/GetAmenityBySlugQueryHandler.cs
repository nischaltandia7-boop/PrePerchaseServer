using MediatR;
using PrePerchaseServer.Models.amenities.repository;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Queries.GetAmenities;
public class GetAmenityBySlugQueryHandler
    : IRequestHandler<GetAmenityBySlugQuery, AmenitiesResponseDto?>
{
    private readonly IAmenitiesRepository _repo;

    public GetAmenityBySlugQueryHandler(IAmenitiesRepository repo)
    {
        _repo = repo;
    }

    public async Task<AmenitiesResponseDto?> Handle(
        GetAmenityBySlugQuery request,
        CancellationToken cancellationToken)
    {
        var amenity = await _repo.GetBySlugAsync(request.Slug);

        if (amenity == null)
            return null;

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