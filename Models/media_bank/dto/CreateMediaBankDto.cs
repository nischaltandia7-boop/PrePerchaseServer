// DTOs/CreateMediabankDto.cs
using PrePerchaseServer.Models.amenities;
using PrePerchaseServer.Models.hotel;
using PrePerchaseServer.Models.hotelgroup;
using PrePerchaseServer.Models.roomcategory;

public class CreateMediabankDto
{
    public string Module { get; set; } = string.Empty;
    public string? Submodule { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
}