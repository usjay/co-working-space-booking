using coreworking_space_booking_backend.Models;

namespace coreworking_space_booking_backend.Dtos.Responses.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string OperationHours { get; set; }
        public int Capacity { get; set; }
    }
}
