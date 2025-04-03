using Microsoft.AspNetCore.Mvc;
using u_navigator_backend.Application.DTOs;
using u_navigator_backend.Application.Interfaces;


namespace u_navigator_backend.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var token = await _authService.RegisterAsync(registerDto);
            if (token == null) return BadRequest(new { message = "Email already exists" });

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null) return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { token });
        }
    }
}
