using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.Pricing
{
    public class PricingRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Hourly { get; set; }

        [Required]
        public decimal Daily { get; set; }

        [Required]
        public decimal Monthly { get; set; }

        public decimal Yearly { get; set; }
    }

    public class UpdatePricingRequest : PricingRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class GetPricingByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class DeletePricingRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
