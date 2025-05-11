using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CustomerDataPlatform.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        //public string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public List<string> AddressIds { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
    }
}
