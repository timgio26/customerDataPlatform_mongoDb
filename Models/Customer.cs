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
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<Address> AddressList { get; set; } = new List<Address>();
    }
}
