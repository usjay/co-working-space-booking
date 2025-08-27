using System.Collections.Generic;
using coreworking_space_booking_backend.Dtos.Requests.Rating;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Services.RatingService;
using Microsoft.AspNetCore.Mvc;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("add-rating")]
        public IActionResult AddRating([FromBody] AddRatingRequest request)
        {
            BaseResponse<string> result = _ratingService.AddRating(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("update-rating")]
        public IActionResult UpdateRating([FromBody] UpdateRatingRequest request)
        {
            BaseResponse<string> result = _ratingService.UpdateRating(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-rating-by-id")]
        public IActionResult GetRatingById([FromBody] GetRatingByIdRequest request)
        {
            BaseResponse<RatingResponse> result = _ratingService.GetRatingById(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-ratings-by-product")]
        public IActionResult GetRatingsByProduct([FromBody] GetRatingsByProductRequest request)
        {
            BaseResponse<List<RatingResponse>> result = _ratingService.GetRatingsByProductId(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("delete-rating")]
        public IActionResult DeleteRating([FromBody] DeleteRatingRequest request)
        {
            BaseResponse<string> result = _ratingService.DeleteRating(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("view-all-ratings")]
        public IActionResult ViewAllRatings()
        {
            BaseResponse<List<RatingResponse>> result = _ratingService.GetAllRatings();
            return StatusCode(result.StatusCode, result);
        }
    }
}
