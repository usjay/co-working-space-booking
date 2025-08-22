using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Request.Rating
{
    public class RatingRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public double Value { get; set; }

        [Required]
        public int UserId { get; set; }  // Added
    }

    public class UpdateRatingRequest : RatingRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class GetRatingByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class GetRatingsByProductRequest
    {
        [Required]
        public int ProductId { get; set; }
    }

    public class GetRatingsByUserRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
