using System.ComponentModel.DataAnnotations;
using PrePerchaseServer.Modules.Auth.Enums;

namespace PrePerchaseServer.Modules.Auth.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }

    public UserStatus Status { get; set; } = UserStatus.ACTIVE;
}