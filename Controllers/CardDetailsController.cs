using coreworking_space_booking_backend.Dtos.Requests.CardDetails;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.CardDetails;
using coreworking_space_booking_backend.Services.CardDetailsService;
using coreworking_space_booking_backend.Services.FacilityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardDetailsController : ControllerBase
    {
        private readonly ICardDetailsService _cardDetailsService;
        public CardDetailsController(ICardDetailsService CardDetailsService)
        {
            _cardDetailsService = CardDetailsService;
        }

        [HttpPost("create-card")]
        public IActionResult CreateCard([FromForm] CardCreateRequestDto request)
        {
            BaseResponse<string> result = _cardDetailsService.CreateCard(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-cards-by-user")]
        public IActionResult GetCardsByUser([FromBody] CardDetailsRequestDto request)
        {
            BaseResponse<List<CardListResponseDto>> result = _cardDetailsService.GetCardsByUser(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("update-card")]
        public IActionResult UpdateCard([FromForm] CardUpdateRequestDto request)
        {
            BaseResponse<string> result = _cardDetailsService.UpdateCard(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("delete-card")]
        public IActionResult DeleteCard([FromBody] CardDeleteRequestDto request)
        {
            BaseResponse<string> result = _cardDetailsService.DeleteCard(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
