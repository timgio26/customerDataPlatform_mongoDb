namespace CustomerDataPlatform.Models
{
    public class Address
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string Alamat { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<string> ServiceIds { get; set; } = new List<string>();


    }
}
