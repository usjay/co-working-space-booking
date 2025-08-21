namespace coreworking_space_booking_backend.Dtos.Requests.CardDetails
{
    public class CardUpdateRequestDto
    {
        public required long CardId { get; set; }

        public required long UserId { get; set; }

        public required long CardToken { get; set; }

        public required string CardHolderName { get; set; }

        public required long LastFourDigits { get; set; }

        public required DateOnly ExpiryDate { get; set; }
    }
}
