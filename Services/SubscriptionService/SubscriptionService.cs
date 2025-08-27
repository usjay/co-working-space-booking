using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests;
using coreworking_space_booking_backend.Dtos.Requests.UserSubcription;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private readonly IConfiguration _configuration;
        private const string LogSource = "SubscriptionService";

        public SubscriptionService(ApplicationDbContext context, IAppLogger logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public BaseResponse<string> SubscribeEmail(SubscribeRequest request)
        {
            try
            {
                UserSubscription existing = _context.UserSubscriptions
                    .FirstOrDefault(u => u.Email == request.Email);

                if (existing != null)
                {
                    if (!existing.IsActive)
                    {
                        existing.IsActive = true;
                        existing.UpdatedAt = DateTime.UtcNow;
                        _context.SaveChanges();
                        return BaseResponse<string>.SuccessResponse("Subscription reactivated!");
                    }
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status400BadRequest, "Email already subscribed");
                }

                UserSubscription subscription = new UserSubscription
                {
                    Email = request.Email,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _context.UserSubscriptions.Add(subscription);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Subscribed successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(SubscribeEmail), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<List<string>> GetAllSubscribers()
        {
            try
            {
                List<string> emails = _context.UserSubscriptions
                    .Where(u => u.IsActive)
                    .Select(u => u.Email)
                    .ToList();

                if (emails.Count == 0)
                    return BaseResponse<List<string>>.ErrorResponse(StatusCodes.Status404NotFound, "No subscribers found");

                return BaseResponse<List<string>>.SuccessResponse(emails, "Subscribers retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllSubscribers), ex);
                return BaseResponse<List<string>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<string> UnsubscribeEmail(SubscribeRequest request)
        {
            try
            {
                UserSubscription existing = _context.UserSubscriptions
                    .FirstOrDefault(u => u.Email == request.Email && u.IsActive);

                if (existing == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Email not found or already unsubscribed");

                existing.IsActive = false;
                existing.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Unsubscribed successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UnsubscribeEmail), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
