namespace coreworking_space_booking_backend.Dtos.Responses.Payment
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
    }
}
