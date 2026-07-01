using System;
using PrePerchaseServer.Models.amenities.enums;
using PrePerchaseServer.Models.mediaBank;

namespace PrePerchaseServer.Models.amenities
{
    public class Amenities
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // room, property, etc.
        public Guid? IconId { get; set; }
        public AmenitiesStatus Status { get; set; } = AmenitiesStatus.ACTIVE;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<Mediabank> AmenitiesImages { get; set; } = new();
    }
}