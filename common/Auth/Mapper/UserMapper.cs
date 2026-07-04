using PrePerchaseServer.Modules.Auth.DTOs;
using PrePerchaseServer.Modules.Auth.Entities;

namespace PrePerchaseServer.Modules.Auth.Mappers;

public static class UserMapper
{
    public static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.ToString(),
            Status = user.Status.ToString()
        };
    }
}