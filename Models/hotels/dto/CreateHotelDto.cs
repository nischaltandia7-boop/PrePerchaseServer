using PrePerchaseServer.Models.hotel.enums;
using PrePerchaseServer.Models.roomcategory;
using PrePerchaseServer.Models.roomcategory.dto;

namespace PrePerchaseServer.Models.hotel.dto
{
    public class CreateHotelDto
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
        public HotelStatus Status { get; set; } = HotelStatus.ACTIVE;
        public List<CreateRoomCategoryDto> RoomCategories { get; set; } = new();
        public List<CreateHotelCancellationPolicyDto> CancellationPolicies { get; set; } = new();
        public List<Guid>? HotelImages {get;set;} = new();
    }
}