using u_navigator_backend.Application.DTOs;
using u_navigator_backend.Application.Interfaces;
using u_navigator_backend.Domain.Models;
using u_navigator_backend.Common.Helpers;
using u_navigator_backend.Infrastructure.Repositories.Interfaces;
using u_navigator_backend.Infrastructure.Repositories;

namespace u_navigator_backend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IConsultantRepository _consultantRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IStudentRepository studentRepository, IConsultantRepository consultantRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _consultantRepository = consultantRepository;
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
                PasswordHash = PasswordHasher.HashPassword(registerDto.Password),
                Role = registerDto.Role // Ensure DTO includes Role
            };

            await _userRepository.AddUserAsync(user);

            // Store role-specific data and update user with RoleDataId
            if (user.Role == "Student")
            {
                var student = new Student
                {
                    UserId = user.Id,
                    University = registerDto.University!,
                    Degree = registerDto.Degree!
                };
                await _studentRepository.AddStudentAsync(student);
                user.RoleDataId = student.Id; // Store reference to student data
            }
            else if (user.Role == "Consultant")
            {
                var consultant = new Consultant
                {
                    UserId = user.Id,
                    Expertise = registerDto.Expertise!,
                    YearsOfExperience = registerDto.YearsOfExperience
                };
                await _consultantRepository.AddConsultantAsync(consultant);
                user.RoleDataId = consultant.Id; // Store reference to consultant data
            }

            // Update user document with RoleDataId
            await _userRepository.UpdateUserAsync(user);

            return JwtTokenHelper.GenerateToken(user, _configuration);
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            return JwtTokenHelper.GenerateToken(user, _configuration);
        }
    }
}
