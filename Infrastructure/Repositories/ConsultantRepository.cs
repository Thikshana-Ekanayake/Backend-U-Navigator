using MongoDB.Driver;
using u_navigator_backend.Domain.Models;
using u_navigator_backend.Infrastructure;
using u_navigator_backend.Infrastructure.Repositories.Interfaces;

namespace u_navigator_backend.Infrastructure.Repositories
{
    public class ConsultantRepository : IConsultantRepository
    {
        private readonly IMongoCollection<Consultant> _consultants;

        public ConsultantRepository(MongoDBContext dbContext)
        {
            _consultants = dbContext.Consultants;
        }

        public async Task AddConsultantAsync(Consultant consultant)
        {
            await _consultants.InsertOneAsync(consultant);
        }

        public async Task<Consultant?> GetConsultantByUserIdAsync(string userId)
        {
            return await _consultants.Find(c => c.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
