using coreworking_space_booking_backend.Dtos.Request.Rating;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Services.RatingService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create([FromBody] RatingRequest request)
        {
            return _ratingService.CreateRating(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update([FromBody] UpdateRatingRequest request)
        {
            return _ratingService.UpdateRating(request);
        }

        [HttpPost("delete/{id}")]
        public BaseResponse<string> Delete(int id)
        {
            return _ratingService.DeleteRating(id);
        }

        [HttpPost("get-by-id")]
        public BaseResponse<RatingResponse> GetById([FromBody] GetRatingByIdRequest request)
        {
            return _ratingService.GetRatingById(request);
        }

        [HttpPost("get-by-product")]
        public BaseResponse<List<RatingResponse>> GetByProduct([FromBody] GetRatingsByProductRequest request)
        {
            return _ratingService.GetRatingsByProduct(request);
        }

        [HttpPost("get-by-user")]
        public BaseResponse<List<RatingResponse>> GetByUser([FromBody] GetRatingsByUserRequest request)
        {
            return _ratingService.GetRatingsByUser(request);
        }
    }
}
