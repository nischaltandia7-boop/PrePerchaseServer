using System;

using PrePerchaseServer.Models.stay_highlight.enums;
namespace PrePerchaseServer.Models.stay_highlight;
public class UpdateStayHighlightDto
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public StayStatus  Status { get; set; }
}