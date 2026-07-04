using PrePerchaseServer.Modules.Auth.DTOs;
using PrePerchaseServer.Modules.Auth;
using PrePerchaseServer.Modules.Auth.Entities;

namespace PrePerchaseServer.Modules.Auth.Services;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();

    DateTime GetRefreshTokenExpiry();
}