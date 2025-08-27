using System;

namespace coreworking_space_booking_backend.Dtos.Requests.Product
{
    public class AdvancedFilterRequest
    {
        public string Type { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime? Date { get; set; }
        public int? Capacity { get; set; }
    }
}
