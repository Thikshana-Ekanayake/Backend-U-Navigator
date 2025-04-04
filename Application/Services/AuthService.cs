using u_navigator_backend.Application.DTOs;
using u_navigator_backend.Application.Interfaces;
using u_navigator_backend.Domain.Models;
using u_navigator_backend.Common.Helpers;
using u_navigator_backend.Infrastructure.Repositories;

namespace u_navigator_backend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string?> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null) return null;

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = PasswordHasher.HashPassword(registerDto.Password)
            };

            await _userRepository.AddUserAsync(user);
            return JwtTokenHelper.GenerateToken(user, _configuration); // use helper here
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            return JwtTokenHelper.GenerateToken(user, _configuration); // use helper here
        }

    }
}
