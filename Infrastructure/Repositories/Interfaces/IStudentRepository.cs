using u_navigator_backend.Domain.Models;

namespace u_navigator_backend.Infrastructure.Repositories
{
    public interface IStudentRepository
    {
        Task AddStudentAsync(Student student);
        Task<Student?> GetStudentByUserIdAsync(string userId);
    }
}
