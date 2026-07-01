using PrePerchaseServer.Models.amenities.enums;
using PrePerchaseServer.Models.mediaBank;

namespace PrePerchaseServer.Models.features.dto
{
    public class CreateAmenitiesDto
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public Guid? IconId { get; set; }

        public AmenitiesStatus Status { get; set; } = AmenitiesStatus.ACTIVE;

        public List<Guid> IconIds { get; set; } = new();
    }
}