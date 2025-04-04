using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace u_navigator_backend.Domain.Models
{
    public class Consultant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty; // Reference to User collection

        [BsonElement("expertise")]
        public string Expertise { get; set; } = string.Empty;

        [BsonElement("yearsOfExperience")]
        public int YearsOfExperience { get; set; }
    }
}
