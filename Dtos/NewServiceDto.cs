namespace CustomerDataPlatform.Dtos
{
    public class NewServiceDto
    {
        public string CustomerId { get; set; } = string.Empty;
        public string AddressId { get; set; } = string.Empty;
        public string Keluhan { get; set; } = string.Empty;
        public string Tindakan { get; set; } = string.Empty;
        public string Hasil { get; set; } = string.Empty;
    }
}
