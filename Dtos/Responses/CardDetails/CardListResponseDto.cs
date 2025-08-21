using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Responses.CardDetails
{
    public class CardListResponseDto
    {
        public required long Id { get; set; }
        public required long UserId { get; set; }

        public required long CardToken { get; set; }

        public required string CardHolderName { get; set; }

        public required long LastFourDigits { get; set; }

        public required DateOnly ExpiryDate { get; set; }

    }
}
