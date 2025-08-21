using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace coreworking_space_booking_backend.Dtos.Requests.Facility
{
    public class FacilityCreateRequestDto
    {
        [Required]
        public required long FacilityId { get; set; }

        [Required]
        [MaxLength(128)]
        public required string FacilityName { get; set; }

        public string Description { get; set; }

        public string FacilityCode { get; set; }

        
        public long ProductId { get; set; }

        [Required]
        public required long CompanyId { get; set; }

        [Required]
        public required long LocationId { get; set; }

        [Required]
        public required bool IsDefault { get; set; }


    }
}
