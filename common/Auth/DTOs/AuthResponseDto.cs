namespace PrePerchaseServer.Modules.Auth.DTOs;

public class AuthResponseDto
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
    public bool RequiresPasswordChange { get; set; }

    public DateTime ExpiresAt { get; set; }

    public UserDto User { get; set; } = new();
}