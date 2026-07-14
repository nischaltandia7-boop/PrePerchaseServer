using MediatR;
using PrePerchaseServer.Models.features.dto;

namespace PrePerchaseServer.Models.amenities.Commands.CreateAmenity;

public record CreateAmenityCommand(CreateAmenitiesDto Dto)
    : IRequest<AmenitiesResponseDto>;