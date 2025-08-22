using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Dtos.Requests.Booking
{
    public class BookingCreateRequestDto
    {
        [Required]
        public required long BookingId { get; set; }

        [Required]
        public required long UserId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        public required long LocationId { get; set; }

        [Required]
        public required long PaymentId { get; set; }

        [Required]
        public required DateTime StartTime { get; set; }

        [Required]
        public required DateTime EndTime { get; set; }

        public long FacilityId { get; set; }

        public string FacilityCode { get; set; }

    }
}
