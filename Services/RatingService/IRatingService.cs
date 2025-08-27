using coreworking_space_booking_backend.Dtos.Request.Rating;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.RatingService
{
    public interface IRatingService
    {
        BaseResponse<string> CreateRating(RatingRequest request);
        BaseResponse<string> UpdateRating(UpdateRatingRequest request);
        BaseResponse<string> DeleteRating(int id);
        BaseResponse<RatingResponse> GetRatingById(GetRatingByIdRequest request);
        BaseResponse<List<RatingResponse>> GetRatingsByProduct(GetRatingsByProductRequest request);
        BaseResponse<List<RatingResponse>> GetRatingsByUser(GetRatingsByUserRequest request);
    }
}
