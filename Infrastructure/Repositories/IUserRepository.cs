using u_navigator_backend.Domain.Models;

namespace u_navigator_backend.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
