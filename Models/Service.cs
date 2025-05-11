namespace CustomerDataPlatform.Models
{
    public class Service
    {
        public string Id { get; set; } = string.Empty;
        public string Keluhan { get; set; } = string.Empty;
        public string Tindakan { get; set; } = string.Empty;
        public string Hasil { get; set; } = string.Empty;
        public DateOnly ServiceDate { get; set; }
    }
}
