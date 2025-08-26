using Microsoft.AspNetCore.Http;

namespace coreworking_space_booking_backend.Dtos.Requests.Advertising
{
    public class CreateAdvertisingRequest
    {
        public string CompanyName { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
    }

    public class UpdateAdvertisingRequest
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
    }

    public class GetAdvertisingByIdRequest
    {
        public long Id { get; set; }
    }

    public class DisableAdvertisingRequest
    {
        public long Id { get; set; }
    }
}
