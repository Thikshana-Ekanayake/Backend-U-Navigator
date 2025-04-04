using u_navigator_backend.Domain.Models;

namespace u_navigator_backend.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userId);
    }
}
