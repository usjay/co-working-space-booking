using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.Payment
{
    public class PaymentRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }
    }

    public class UpdatePaymentRequest
    {
        [Required]
        public int PaymentId { get; set; }

        public bool IsPaid { get; set; }
    }

    public class GetPaymentByIdRequest
    {
        [Required]
        public int PaymentId { get; set; }
    }
}
