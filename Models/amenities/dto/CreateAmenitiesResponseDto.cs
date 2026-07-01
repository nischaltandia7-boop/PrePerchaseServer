using System;
using PrePerchaseServer.Models.amenities.enums;
using PrePerchaseServer.Models.mediaBank;

namespace PrePerchaseServer.Models.features.dto
{
    public class AmenitiesResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public Guid? IconId { get; set; }

        public AmenitiesStatus Status { get; set; } = AmenitiesStatus.ACTIVE;
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<MediabankResponseDto> AmenitiesIcons { get; set; } = new();

    }
}