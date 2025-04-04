using u_navigator_backend.Domain.Models;
using u_navigator_backend.Application.Interfaces;
using u_navigator_backend.Infrastructure.Repositories.Interfaces;
using u_navigator_backend.Infrastructure.Repositories;

namespace u_navigator_backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IConsultantRepository _consultantRepository;

        public UserService(IUserRepository userRepository, IStudentRepository studentRepository, IConsultantRepository consultantRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _consultantRepository = consultantRepository;
        }

        public async Task<object?> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            if (user.Role == "Student")
            {
                var student = await _studentRepository.GetStudentByUserIdAsync(userId);
                return new { user, student };
            }
            else if (user.Role == "Consultant")
            {
                var consultant = await _consultantRepository.GetConsultantByUserIdAsync(userId);
                return new { user, consultant };
            }

            return user;
        }
    }
}
