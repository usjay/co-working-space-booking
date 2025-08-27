using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Request.Rating;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.RatingService
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private readonly string _logSource;

        public RatingService(ApplicationDbContext context, IAppLogger appLogger)
        {
            _context = context;
            _appLogger = appLogger;
            _logSource = GetType().Name;
        }

        public BaseResponse<string> CreateRating(RatingRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CreateRating), request);

                Rating rating = new Rating
                {
                    ProductId = request.ProductId,
                    UserId = request.UserId,
                    Value = request.Value,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Ratings.Add(rating);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Rating created successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CreateRating), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> UpdateRating(UpdateRatingRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(UpdateRating), request);

                Rating rating = _context.Ratings.FirstOrDefault(r => r.Id == request.Id);
                if (rating == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Rating not found.");

                rating.ProductId = request.ProductId;
                rating.UserId = request.UserId;
                rating.Value = request.Value;
                rating.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Rating updated successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(UpdateRating), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> DeleteRating(int id)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(DeleteRating), id);

                Rating rating = _context.Ratings.FirstOrDefault(r => r.Id == id);
                if (rating == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Rating not found.");

                _context.Ratings.Remove(rating);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Rating deleted successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(DeleteRating), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<RatingResponse> GetRatingById(GetRatingByIdRequest request)
        {
            try
            {
                Rating rating = _context.Ratings.FirstOrDefault(r => r.Id == request.Id);
                if (rating == null)
                    return BaseResponse<RatingResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Rating not found.");

                var response = MapRatingResponse(rating);
                return BaseResponse<RatingResponse>.SuccessResponse(response, "Rating retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetRatingById), ex);
                return BaseResponse<RatingResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<List<RatingResponse>> GetRatingsByProduct(GetRatingsByProductRequest request)
        {
            try
            {
                var ratings = _context.Ratings
                    .Where(r => r.ProductId == request.ProductId)
                    .Select(r => MapRatingResponse(r))
                    .ToList();

                if (!ratings.Any())
                    return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No ratings found for this product");

                return BaseResponse<List<RatingResponse>>.SuccessResponse(ratings, "Ratings retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetRatingsByProduct), ex);
                return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<List<RatingResponse>> GetRatingsByUser(GetRatingsByUserRequest request)
        {
            try
            {
                var ratings = _context.Ratings
                    .Where(r => r.UserId == request.UserId)
                    .Select(r => MapRatingResponse(r))
                    .ToList();

                if (!ratings.Any())
                    return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No ratings found for this user");

                return BaseResponse<List<RatingResponse>>.SuccessResponse(ratings, "Ratings retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetRatingsByUser), ex);
                return BaseResponse<List<RatingResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        // Helper method to map Rating to RatingResponse with full user name
        private RatingResponse MapRatingResponse(Rating rating)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == rating.UserId);
            return new RatingResponse
            {
                //Id = rating.Id,
                //ProductId = rating.ProductId,
                //UserId = rating.UserId,
                UserName = user != null ? $"{user.FirstName} {user.LastName}" : "Unknown",
                Value = rating.Value,
              
            };
        }
    }
}
