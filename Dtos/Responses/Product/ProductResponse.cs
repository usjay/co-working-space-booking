using System.Collections.Generic;
using coreworking_space_booking_backend.Dtos.Responses.Facility;

namespace coreworking_space_booking_backend.Dtos.Responses.Product
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int LocationId { get; set; }
        public string OperationHours { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public List<FacilityDetailsResponseDto> Facilities { get; set; }
        public List<string> Images { get; set; }
       
        public string Availability { get; set; }
        public double AverageRating { get; set; }
    }
}
