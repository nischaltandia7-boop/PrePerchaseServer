using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Modules.Auth.DTOs;
using PrePerchaseServer.Modules.Auth.Enums;
using PrePerchaseServer.Modules.Auth.Services;

namespace PrePerchaseServer.Modules.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        [FromBody] LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);

        if (!response.Success)
            return Unauthorized(response);

        return Ok(response);
    }

    /// <summary>
    /// Create User (Admin Only)
    /// </summary>
    [Authorize(Roles = nameof(UserRole.ADMIN))]
    [HttpPost("register")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterDto dto)
    {
        var response = await _authService.RegisterAsync(dto);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Refresh JWT
    /// </summary>
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(
        [FromBody] RefreshTokenDto dto)
    {
        var response = await _authService.RefreshTokenAsync(dto);

        if (!response.Success)
            return Unauthorized(response);

        return Ok(response);
    }

    /// <summary>
    /// Current Logged In User
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Me()
    {
        var user = await _authService.GetCurrentUserAsync(User);

        return Ok(user);
    }

    /// <summary>
    /// Logout
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _authService.LogoutAsync(User);

        return Ok(new
        {
            success = true,
            message = "Logged out successfully."
        });
    }
}