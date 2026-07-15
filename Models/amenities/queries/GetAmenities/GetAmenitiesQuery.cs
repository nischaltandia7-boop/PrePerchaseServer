using MediatR;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Queries.GetAmenities;
public record GetAmenitiesQuery()
    : IRequest<List<AmenitiesResponseDto>>;