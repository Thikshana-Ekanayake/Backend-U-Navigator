using MongoDB.Driver;
using u_navigator_backend.Domain.Models;
using u_navigator_backend.Infrastructure;

namespace u_navigator_backend.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoCollection<Student> _students;

        public StudentRepository(MongoDBContext dbContext)
        {
            _students = dbContext.Students;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _students.InsertOneAsync(student);
        }

        public async Task<Student?> GetStudentByUserIdAsync(string userId)
        {
            return await _students.Find(s => s.UserId == userId).FirstOrDefaultAsync();
        }
    }

}
