using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;
using PrePerchaseServer.Modules.Auth.Entities;
using PrePerchaseServer.Modules.Auth.Enums;
using PrePerchaseServer.Modules.Auth.Helpers;

namespace PrePerchaseServer.Modules.Auth.Seed;

public static class AuthSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Check if an admin user already exists
        var adminExists = await context.Users
            .AnyAsync(x => x.Role == UserRole.ADMIN);

        if (adminExists)
            return;

        var admin = new User
        {
            Id = Guid.NewGuid(),

            Username = "admin",

            PasswordHash = PasswordHasher.HashPassword("Admin@123"),

            FullName = "System Administrator",

            Email = "admin@prepurchase.com",

            PhoneNumber = "9999999999",

            Role = UserRole.ADMIN,

            Status = UserStatus.ACTIVE,

            RefreshToken = null,

            RefreshTokenExpiry = null,

            CreatedAt = DateTime.UtcNow,

            UpdatedAt = DateTime.UtcNow
        };

        await context.Users.AddAsync(admin);

        await context.SaveChangesAsync();

        Console.WriteLine("✔ Default Admin User Created");
    }
}