using PrePerchaseServer.Modules.Auth.Entities;

namespace PrePerchaseServer.Modules.Auth.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByUsernameAsync(string username);

    Task<User?> GetByEmailAsync(string email);

    Task<bool> UsernameExistsAsync(string username);

    Task<bool> EmailExistsAsync(string email);

    Task<List<User>> GetAllAsync();

    Task<User> CreateAsync(User user);

    Task<User> UpdateAsync(User user);

    Task DeleteAsync(Guid id);

    Task SaveChangesAsync();
}