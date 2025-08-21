using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Responses.Facility
{
    public class FacilityDetailsResponseDto
    {
        public required long FacilityId { get; set; }

        public required string FacilityName { get; set; }

        public string Description { get; set; }

        public string FacilityCode { get; set; }


        public long ProductId { get; set; }

        public required long CompanyId { get; set; }

        public required long LocationId { get; set; }

        public required bool IsDefault { get; set; }
    }
}
