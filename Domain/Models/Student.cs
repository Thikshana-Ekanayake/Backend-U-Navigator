using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace u_navigator_backend.Domain.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty; // Reference to User collection

        [BsonElement("university")]
        public string University { get; set; } = string.Empty;

        [BsonElement("degree")]
        public string Degree { get; set; } = string.Empty;
    }
}
