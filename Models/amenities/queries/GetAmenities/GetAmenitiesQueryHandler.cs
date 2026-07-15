using MediatR;
using PrePerchaseServer.Models.amenities;
using PrePerchaseServer.Models.amenities.repository;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Queries.GetAmenities;
public class GetAmenitiesQueryHandler
    : IRequestHandler<GetAmenitiesQuery, List<AmenitiesResponseDto>>
{
    private readonly IAmenitiesRepository _repo;

    public GetAmenitiesQueryHandler(IAmenitiesRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AmenitiesResponseDto>> Handle(
        GetAmenitiesQuery request,
        CancellationToken cancellationToken)
    {
        var amenities = await _repo.GetAllAsync();

        return amenities.Select(Map).ToList();
    }
    private static AmenitiesResponseDto Map(Amenities amenity)
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
            UpdatedAt = amenity.UpdatedAt
        };
    }
}