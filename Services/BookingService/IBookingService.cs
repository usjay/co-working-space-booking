using coreworking_space_booking_backend.Dtos.Requests.Booking;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Booking;

namespace coreworking_space_booking_backend.Services.BookingService
{
    public interface IBookingService
    {
        public BaseResponse<string> CreateBooking(BookingCreateRequestDto request);
        public BaseResponse<BookingDetailsResponseDto> GetBookingById(BookingDetailsRequestDto request);
        public BaseResponse<List<BookingListResponseDto>> GetBookingList();
        public BaseResponse<string> UpdateBooking(BookingUpdateRequestDto request);
        public BaseResponse<string> CanselBooking(BookingCanselRequestDto request);
    }
}
