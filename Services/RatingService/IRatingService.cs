using System.Collections.Generic;
using coreworking_space_booking_backend.Dtos.Requests.Rating;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Rating;

namespace coreworking_space_booking_backend.Services.RatingService
{
    public interface IRatingService
    {
        BaseResponse<string> AddRating(AddRatingRequest request);
        BaseResponse<string> UpdateRating(UpdateRatingRequest request);
        BaseResponse<RatingResponse> GetRatingById(GetRatingByIdRequest request);
        BaseResponse<List<RatingResponse>> GetRatingsByProductId(GetRatingsByProductRequest request);
        BaseResponse<string> DeleteRating(DeleteRatingRequest request);
        BaseResponse<List<RatingResponse>> GetAllRatings();
    }
}
