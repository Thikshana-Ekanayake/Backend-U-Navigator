using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace u_navigator_backend.Domain.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = "User"; // Default role

        [BsonElement("roleDataId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RoleDataId { get; set; } // Reference to the role-specific collection
    }
}
