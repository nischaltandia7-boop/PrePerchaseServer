using System.Security.Claims;
using PrePerchaseServer.Modules.Auth.DTOs;
using PrePerchaseServer.Modules.Auth.Entities;
using PrePerchaseServer.Modules.Auth.Enums;
using PrePerchaseServer.Modules.Auth.Helpers;
using PrePerchaseServer.Modules.Auth.Mappers;
using PrePerchaseServer.Modules.Auth.Repositories;

namespace PrePerchaseServer.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);

        if (user == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid username or password."
            };
        }

        if (user.Status != UserStatus.ACTIVE)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "User is inactive."
            };
        }

        if (!PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid username or password."
            };
        }

        user.RefreshToken = _jwtTokenService.GenerateRefreshToken();
        user.RefreshTokenExpiry = _jwtTokenService.GetRefreshTokenExpiry();
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);

        return new AuthResponseDto
        {
            Success = true,
            Message = user.MustChangePassword
                ? "Password change required."
                : "Login successful.",

            AccessToken = _jwtTokenService.GenerateAccessToken(user),
            RefreshToken = user.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            RequiresPasswordChange = user.MustChangePassword,
            User = UserMapper.MapToDto(user)
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        if (await _userRepository.UsernameExistsAsync(dto.Username))
            throw new Exception("Username already exists.");

        if (await _userRepository.EmailExistsAsync(dto.Email))
            throw new Exception("Email already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,

            // Keeping your current implementation
            PasswordHash = PasswordHasher.HashPassword(dto.Email),

            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role,
            Status = dto.Status,

            MustChangePassword = true,

            RefreshToken = _jwtTokenService.GenerateRefreshToken(),
            RefreshTokenExpiry = _jwtTokenService.GetRefreshTokenExpiry(),

            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.CreateAsync(user);

        return new AuthResponseDto
        {
            Success = true,
            Message = "User created successfully.",

            AccessToken = _jwtTokenService.GenerateAccessToken(user),

            RefreshToken = user.RefreshToken!,

            ExpiresAt = DateTime.UtcNow.AddMinutes(60),

            RequiresPasswordChange = true,

            User = UserMapper.MapToDto(user)
        };
    }

    public async Task<AuthResponseDto> ChangePasswordAsync(
        ClaimsPrincipal principal,
        ChangePasswordDto dto)
    {
        var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(id))
            throw new Exception("Invalid user.");

        var user = await _userRepository.GetByIdAsync(Guid.Parse(id));

        if (user == null)
            throw new Exception("User not found.");

        if (!PasswordHasher.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
            throw new Exception("Current password is incorrect.");

        user.PasswordHash = PasswordHasher.HashPassword(dto.NewPassword);
        user.MustChangePassword = false;
        user.PasswordChangedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Password changed successfully.",

            AccessToken = _jwtTokenService.GenerateAccessToken(user),

            RefreshToken = user.RefreshToken!,

            ExpiresAt = DateTime.UtcNow.AddMinutes(60),

            RequiresPasswordChange = false,

            User = UserMapper.MapToDto(user)
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(id))
            return null;

        var user = await _userRepository.GetByIdAsync(Guid.Parse(id));

        return user == null ? null : UserMapper.MapToDto(user);
    }

    public async Task LogoutAsync(ClaimsPrincipal principal)
    {
        var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(id))
            return;

        var user = await _userRepository.GetByIdAsync(Guid.Parse(id));

        if (user == null)
            return;

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
    }
}