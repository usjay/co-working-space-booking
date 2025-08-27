using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.Rating
{
    public class AddRatingRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating value must be between 1 and 5")]
        public double Value { get; set; }

        public string ReviewDescription { get; set; }
    }

    public class UpdateRatingRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating value must be between 1 and 5")]
        public double Value { get; set; }

        public string ReviewDescription { get; set; }
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

    public class DeleteRatingRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
