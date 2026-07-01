using PrePerchaseServer.Models.hotel.enums;

namespace PrePerchaseServer.Models.hotel
{
    public class HotelCancellationPolicy
    {
        public Guid Id { get; set; }

        public Guid HotelId { get; set; }

        public CancellationPolicyType PolicyType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Hotel Hotel { get; set; } = null!;

        public List<HotelCancellationPolicySlab> Slabs { get; set; } = new();
    }
}