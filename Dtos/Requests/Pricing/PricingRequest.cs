using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Dtos.Requests.Pricing
{
    public class PricingRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Column("location_id")]
        [Required]
        public int LocationId { get; set; }

       
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
