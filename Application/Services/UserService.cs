using u_navigator_backend.Domain.Models;
using u_navigator_backend.Infrastructure.Repositories;
using u_navigator_backend.Application.Interfaces;

namespace u_navigator_backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}
