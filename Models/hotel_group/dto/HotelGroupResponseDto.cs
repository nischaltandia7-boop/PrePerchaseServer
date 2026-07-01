using PrePerchaseServer.Models.hotelgroup.enums;
using PrePerchaseServer.Models.mediaBank;

namespace PrePerchaseServer.Models.hotelgroup.dto;

public class HotelGroupResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? LogoId { get; set; }
    public HotelGroupStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Mediabank> HotelGroupImg { get; set; } = new();
}
