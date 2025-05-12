using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CustomerDataPlatform.Models
{
    public class Address
    {
        public string Id { get; set; } = string.Empty;
        public string Alamat { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<Service> ServiceList { get; set; } = new List<Service>();


    }
}
