namespace PrePerchaseServer.Models.hotel
{
    public class HotelCancellationPolicySlab
    {
        public Guid Id { get; set; }

        public Guid PolicyId { get; set; }

        public string TimeRange { get; set; } = string.Empty;

        public string ChargeType { get; set; } = string.Empty;

        public decimal Value { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public HotelCancellationPolicy Policy { get; set; } = null!;
    }
}