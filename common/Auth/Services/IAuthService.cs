using System.Security.Claims;
using PrePerchaseServer.Modules.Auth.DTOs;

namespace PrePerchaseServer.Modules.Auth.Services;
public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
    Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal user);
    Task LogoutAsync(ClaimsPrincipal user);
}