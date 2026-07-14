using MediatR;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Queries.GetAmenities;
public record GetAmenityBySlugQuery(string Slug)
    : IRequest<AmenitiesResponseDto?>;