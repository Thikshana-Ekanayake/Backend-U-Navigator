﻿using MongoDB.Driver;
using u_navigator_backend.Domain.Models;
using u_navigator_backend.Infrastructure;
using u_navigator_backend.Infrastructure.Repositories.Interfaces;

namespace u_navigator_backend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDBContext dbContext)
        {
            _users = dbContext.Users;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            await _users.ReplaceOneAsync(filter, user);
        }
    }
}
