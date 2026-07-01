using PrePerchaseServer.Models.hotel.enums;
using PrePerchaseServer.Models.roomcategory;

namespace PrePerchaseServer.Models.hotel.dto
{
    public class UpdateHotelDto
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? StateId { get; set; }
        public string CityId { get; set; } = string.Empty;
        public string HotelGroupId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? AboutHotel { get; set; }
        public string CheckInTime { get; set; } = string.Empty;
        public string CheckOutTime { get; set; } = string.Empty;
        public decimal? CentralLatitude { get; set; }
        public decimal? CentralLongitude { get; set; }
        public List<string> Amenities { get; set; } = new();
        public List<string> StayHighlights { get; set; } = new();
        public HotelStatus Status { get; set; }
        public List<RoomCategory> RoomCategories { get; set; } = new();
        public List<HotelCancellationPolicy> CancellationPolicies { get; set; } = new();
        public List<Guid>? HotelImages { get; set; } = new();
    }
}