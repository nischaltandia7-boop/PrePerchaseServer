using System;
using PrePerchaseServer.Models.stay_highlight.enums;

namespace PrePerchaseServer.Models.stay_highlight.dto;
public class StayHighlightResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public StayStatus Status { get; set; } 

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}