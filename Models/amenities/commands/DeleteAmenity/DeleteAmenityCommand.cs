using MediatR;

namespace PrePerchaseServer.Models.amenities.Commands.DeleteAmenity
{
    public record DeleteAmenityCommand(string Slug) : IRequest<bool>;
}