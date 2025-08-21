namespace coreworking_space_booking_backend.Dtos.Responses.Pricing
{
    public class PricingResponse
    {
        public int Id { get; set; }
        //public int ProductId { get; set; }
        public decimal Hourly { get; set; }
        public decimal Daily { get; set; }
        public decimal Monthly { get; set; }
        public decimal Yearly { get; set; }
    }
}
