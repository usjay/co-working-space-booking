using coreworking_space_booking_backend.Dtos.Requests.Booking;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Booking;
using coreworking_space_booking_backend.Services.BookingService;
using coreworking_space_booking_backend.Services.FacilityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService BookingService)
        {
            _bookingService = BookingService;
        }

        [HttpPost("create-booking")]
        public IActionResult CreateFacility([FromForm] BookingCreateRequestDto request)
        {
            BaseResponse<string> result = _bookingService.CreateBooking(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-booking")]
        public IActionResult GetBookingById([FromBody] BookingDetailsRequestDto request)
        {
            BaseResponse<BookingDetailsResponseDto> result = _bookingService.GetBookingById(request);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("get-booking-list")]
        public IActionResult GetFacilityList()
        {
            BaseResponse<List<BookingListResponseDto>> result = _bookingService.GetBookingList();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("update-boooking")]
        public IActionResult UpdateFacility([FromForm] BookingUpdateRequestDto request)
        {
            BaseResponse<string> result = _bookingService.UpdateBooking(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("cansel-booking")]
        public IActionResult DeleteFacility([FromBody] BookingCanselRequestDto request)
        {
            BaseResponse<string> result = _bookingService.CanselBooking(request);
            return StatusCode(result.StatusCode, result);
        }

    }
}
