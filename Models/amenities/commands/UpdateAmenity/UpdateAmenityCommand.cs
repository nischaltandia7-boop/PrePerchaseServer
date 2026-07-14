using MediatR;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Commands.UpdateAmenity
{
    public record UpdateAmenityCommand(
        string Slug,
        CreateAmenitiesDto Dto
    ) : IRequest<AmenitiesResponseDto?>;
}