using PrePerchaseServer.Models.hotel.enums;

namespace PrePerchaseServer.Models.hotel.dto
{
    public class CreateHotelCancellationPolicyDto
    {
        public CancellationPolicyType PolicyType { get; set; }

        public List<CreateHotelCancellationPolicySlabDto> Slabs { get; set; } = new();
    }
}