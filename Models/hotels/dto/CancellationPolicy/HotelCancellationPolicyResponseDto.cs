using PrePerchaseServer.Models.hotel.enums;

public class HotelCancellationPolicyResponseDto
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public CancellationPolicyType PolicyType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<HotelCancellationPolicySlabResponseDto> Slabs { get; set; } = new();
}