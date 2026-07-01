using PrePerchaseServer.Models.amenities.enums;

namespace PrePerchaseServer.Models.features.dto
{
    public class UpdateAmenitiesDto
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public Guid? IconId { get; set; }

        public AmenitiesStatus Status { get; set; }

        public List<Guid> IconIds { get; set; } = new();
    }
}