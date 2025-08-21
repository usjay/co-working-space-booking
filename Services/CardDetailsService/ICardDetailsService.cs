using coreworking_space_booking_backend.Dtos.Requests.CardDetails;
using coreworking_space_booking_backend.Dtos.Requests.Facility;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.CardDetails;
using coreworking_space_booking_backend.Dtos.Responses.Facility;

namespace coreworking_space_booking_backend.Services.CardDetailsService
{
    public interface ICardDetailsService
    {
        public BaseResponse<string> CreateCard(CardCreateRequestDto request);
        public BaseResponse<List<CardListResponseDto>> GetCardsByUser(CardDetailsRequestDto request);
        public BaseResponse<string> UpdateCard(CardUpdateRequestDto request);
        public BaseResponse<string> DeleteCard(CardDeleteRequestDto request);
    }
}
