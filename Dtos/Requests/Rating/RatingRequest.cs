using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.Rating
{
    public class RatingRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Value { get; set; }
    }
}
