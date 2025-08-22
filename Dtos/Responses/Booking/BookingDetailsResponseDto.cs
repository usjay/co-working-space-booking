using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Dtos.Responses.Booking
{
    public class BookingDetailsResponseDto
    {
        public required long BookingId { get; set; }

        public required long UserId { get; set; }

        public long ProductId { get; set; }

        public required long LocationId { get; set; }

        public required long PaymentId { get; set; }

        public long FacilityId { get; set; }

        public string FacilityCode { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsCanseled { get; set; }

        public required bool IsOnetimeChanged { get; set; }
    }
}
