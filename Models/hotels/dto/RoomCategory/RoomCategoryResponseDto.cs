using PrePerchaseServer.Models.mediaBank;
using PrePerchaseServer.Models.roomcategory.enums;

namespace PrePerchaseServer.Models.roomcategory.dto
{
    public class RoomCategoryResponseDto
    {
        public Guid Id { get; set; }

        public Guid HotelId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Slug { get; set; }

        public List<string> RoomAmenities { get; set; } = new();

        public int AdultCount { get; set; }

        public int? ChildCount { get; set; }

        public int? MaxChildAge { get; set; }

        public int ExtraBedCount { get; set; }

        public int? RoomCategoryId { get; set; }

        public RoomCategoryStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public List<MediabankResponseFileDto> RoomImg {get;set;} = new ();
    }
}