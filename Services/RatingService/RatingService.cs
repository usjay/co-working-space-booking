using System;
using System.Collections.Generic;
using System.Linq;
using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Rating;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;

namespace coreworking_space_booking_backend.Services.RatingService
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private const string LogSource = "RatingService";

        public RatingService(ApplicationDbContext context, IAppLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public BaseResponse<string> AddRating(AddRatingRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(AddRating), request);

                Rating rating = new Rating();
                rating.ProductId = request.ProductId;
                rating.UserId = request.UserId;
                rating.Value = request.Value;
                rating.ReviewDescription = request.ReviewDescription ?? string.Empty;
                rating.IsDeleted = false;
                rating.CreatedAt = DateTime.UtcNow;

                _context.Ratings.Add(rating);
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(AddRating));
                return BaseResponse<string>.SuccessResponse("Rating added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(AddRating), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<string> UpdateRating(UpdateRatingRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(UpdateRating), request);

                Rating rating = _context.Ratings.FirstOrDefault(r => r.Id == request.Id && r.UserId == request.UserId && !r.IsDeleted);
                if (rating == null)
                {
                    _logger.LogMethodStop(LogSource, nameof(UpdateRating));
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Rating not found");
                }

                rating.Value = request.Value;
                rating.ReviewDescription = request.ReviewDescription ?? string.Empty;
                rating.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(UpdateRating));
                return BaseResponse<string>.SuccessResponse("Rating updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdateRating), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<List<RatingResponse>> GetRatingsByProductId(GetRatingsByProductRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetRatingsByProductId), request);

                List<Rating> ratingsFromDb = _context.Ratings
                    .Where(r => r.ProductId == request.ProductId && !r.IsDeleted)
                    .OrderByDescending(r => r.Value)
                    .ToList();

                if (!ratingsFromDb.Any())
                {
                    _logger.LogMethodStop(LogSource, nameof(GetRatingsByProductId));
                    return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No ratings found");
                }

                List<RatingResponse> ratings = ratingsFromDb
                    .Select(r => new RatingResponse
                    {
                        Value = r.Value,
                        ReviewDescription = r.ReviewDescription,
                        UserId = r.UserId,
                        UserAvatar = r.User != null ? r.User.Avatar : string.Empty
                    })
                    .ToList();

                _logger.LogMethodStop(LogSource, nameof(GetRatingsByProductId));
                return BaseResponse<List<RatingResponse>>.SuccessResponse(ratings, "Ratings retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetRatingsByProductId), ex);
                return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<RatingResponse> GetRatingById(GetRatingByIdRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetRatingById), request);

                Rating rating = _context.Ratings.FirstOrDefault(r => r.Id == request.Id && !r.IsDeleted);
                if (rating == null)
                {
                    _logger.LogMethodStop(LogSource, nameof(GetRatingById));
                    return BaseResponse<RatingResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Rating not found");
                }

                RatingResponse response = new RatingResponse();
                response.Value = rating.Value;
                response.ReviewDescription = rating.ReviewDescription;
                response.UserId = rating.UserId;
                response.UserAvatar = rating.User != null ? rating.User.Avatar : string.Empty;

                _logger.LogMethodStop(LogSource, nameof(GetRatingById));
                return BaseResponse<RatingResponse>.SuccessResponse(response, "Rating retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetRatingById), ex);
                return BaseResponse<RatingResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<string> DeleteRating(DeleteRatingRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(DeleteRating), request);

                Rating rating = _context.Ratings.FirstOrDefault(r => r.Id == request.Id && !r.IsDeleted);
                if (rating == null)
                {
                    _logger.LogMethodStop(LogSource, nameof(DeleteRating));
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Rating not found");
                }

                rating.IsDeleted = true;
                rating.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(DeleteRating));
                return BaseResponse<string>.SuccessResponse("Rating deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeleteRating), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<List<RatingResponse>> GetAllRatings()
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetAllRatings));

                List<Rating> ratingsFromDb = _context.Ratings
                    .Where(r => !r.IsDeleted)
                    .OrderByDescending(r => r.Value)
                    .ToList();

                if (!ratingsFromDb.Any())
                {
                    _logger.LogMethodStop(LogSource, nameof(GetAllRatings));
                    return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No ratings found");
                }

                List<RatingResponse> ratings = ratingsFromDb
                    .Select(r => new RatingResponse
                    {
                        Value = r.Value,
                        ReviewDescription = r.ReviewDescription,
                        UserId = r.UserId,
                        UserAvatar = r.User != null ? r.User.Avatar : string.Empty
                    })
                    .ToList();

                _logger.LogMethodStop(LogSource, nameof(GetAllRatings));
                return BaseResponse<List<RatingResponse>>.SuccessResponse(ratings, "Ratings retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllRatings), ex);
                return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
