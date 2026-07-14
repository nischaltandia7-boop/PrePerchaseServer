using MediatR;
using PrePerchaseServer.Models.amenities.repository;

namespace PrePerchaseServer.Models.amenities.Commands.DeleteAmenity
{
    public class DeleteAmenityCommandHandler
        : IRequestHandler<DeleteAmenityCommand, bool>
    {
        private readonly IAmenitiesRepository _repo;

        public DeleteAmenityCommandHandler(IAmenitiesRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(
            DeleteAmenityCommand request,
            CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Slug);
        }
    }
}