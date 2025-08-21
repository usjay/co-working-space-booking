using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.CardDetails
{
    public class CardCreateRequestDto
    {
       
        [Required]
        public required long UserId{ get; set; }

        [Required]
        public required long CardToken { get; set; }

        [Required]
        public required string CardHolderName { get; set; }

        [Required]
        public required long LastFourDigits { get; set; }

        [Required]
        public required DateOnly ExpiryDate { get; set; }

    }
}
