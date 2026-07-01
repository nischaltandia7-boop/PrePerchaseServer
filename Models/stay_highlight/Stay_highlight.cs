using System;
using PrePerchaseServer.Models.stay_highlight.enums;

namespace PrePerchaseServer.Models.stay_highlight;

public class StayHighlight
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public StayStatus Status { get; set; } = StayStatus.ACTIVE;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}