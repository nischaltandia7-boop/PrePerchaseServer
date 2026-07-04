using BCrypt.Net;

namespace PrePerchaseServer.Modules.Auth.Helpers;

public static class PasswordHasher
{
    /// <summary>
    /// Hash a plain text password.
    /// </summary>
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verify a plain text password against its hash.
    /// </summary>
    public static bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}