using Microsoft.Extensions.Options;
using MongoDB.Driver;
using u_navigator_backend.Infrastructure.Configurations;
using u_navigator_backend.Domain.Models;

namespace u_navigator_backend.Infrastructure
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Student> Students => _database.GetCollection<Student>("Users_Studnets");
        public IMongoCollection<Consultant> Consultants => _database.GetCollection<Consultant>("Users_Consultants");
    }
}
