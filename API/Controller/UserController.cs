using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using u_navigator_backend.Application.Interfaces;

namespace u_navigator_backend.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize] // Require authentication for all endpoints in this controller
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "Invalid token" });

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }
    }
}
