using PrePerchaseServer.Models.hotel.enums;
using PrePerchaseServer.Models.hotelgroup;
using PrePerchaseServer.Models.mediaBank;
using PrePerchaseServer.Models.roomcategory;
using PrePerchaseServer.Models.roomcategory.dto;

namespace PrePerchaseServer.Models.hotel
{
    public class Hotel
    {
        public Guid Id { get; set; }
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
        public List<HotelGroup> HotelGroups { get; set; } = new();
        public List<Mediabank>? HotelImages { get; set; } = new();
        public List<string> Amenities { get; set; } = new();
        public List<string> StayHighlights { get; set; } = new();
        public HotelStatus Status { get; set; } = HotelStatus.ACTIVE;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<RoomCategory> RoomCategories { get; set; } = new();
        public List<HotelCancellationPolicy> CancellationPolicies { get; set; } = new();
    }
}