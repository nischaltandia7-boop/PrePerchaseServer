using PrePerchaseServer.Models.roomcategory.enums;

namespace PrePerchaseServer.Models.roomcategory.dto
{
    public class CreateRoomCategoryDto
    {
        public Guid HotelId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Slug { get; set; }

        public List<string> RoomAmenities { get; set; } = new();

        public int AdultCount { get; set; }

        public int? ChildCount { get; set; }

        public int? MaxChildAge { get; set; }

        public int ExtraBedCount { get; set; } = 0;

        public int? RoomCategoryId { get; set; }
        public RoomCategoryStatus Status { get; set; }

        public List<Guid>? RoomImg {get;set;} = new ();
    }
}