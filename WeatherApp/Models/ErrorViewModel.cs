namespace Jojo.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string TimeZone { get; set; }
        public string Temperature { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Weather { get; set; }
    }
}