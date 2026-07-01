using PrePerchaseServer.Models.amenities;
using PrePerchaseServer.Models.hotel;
using PrePerchaseServer.Models.hotelgroup;
using PrePerchaseServer.Models.roomcategory;

namespace PrePerchaseServer.Models.mediaBank;
public class Mediabank
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Bucket { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty; // IMAGE / DOCUMENT / VIDEO
    public long Size { get; set; }
    public string Module { get; set; } = string.Empty;
    public string? Submodule { get; set; }
    public string? Type { get; set; } // BANNER / GALLERY etc
    public string Status { get; set; } = "INACTIVE";
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? Orientation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? RoomCategoryId { get; set; }
    public Guid? HotelId { get; set; }
    public Guid? HotelGroupId { get; set; }
    public Guid? AmenitiesId { get; set; }
    public RoomCategory? RoomCategory { get; set; }
    public Hotel? Hotel { get; set; }
    public HotelGroup? HotelGroup { get; set; }
    public Amenities? Amenities { get; set; }
}