namespace PrePerchaseServer.Models.hotel.dto
{
    public class CreateHotelCancellationPolicySlabDto
    {
        public string TimeRange { get; set; } = string.Empty;

        public string ChargeType { get; set; } = string.Empty;

        public decimal Value { get; set; }
    }
}