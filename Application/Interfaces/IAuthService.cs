using u_navigator_backend.Application.DTOs;

namespace u_navigator_backend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(RegisterDto registerDto);
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
