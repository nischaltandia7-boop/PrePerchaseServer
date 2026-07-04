using System.ComponentModel.DataAnnotations;

namespace PrePerchaseServer.Modules.Auth.DTOs;

public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}