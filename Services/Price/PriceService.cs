using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Pricing;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Pricing;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.PricingService
{
    public class PricingService : IPricingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private const string LogSource = "PricingService";

        public PricingService(ApplicationDbContext context, IAppLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public BaseResponse<string> CreatePricing(PricingRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(CreatePricing), request);

                Pricing pricing = new Pricing
                {
                    ProductId = request.ProductId,
                    HourlyRate = request.Hourly,
                    DailyRate = request.Daily,
                    MonthlyRate = request.Monthly,
                    YearlyRate = request.Yearly
                };

                _context.Pricings.Add(pricing);
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(CreatePricing));
                return BaseResponse<string>.SuccessResponse("Pricing created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(CreatePricing), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> UpdatePricing(UpdatePricingRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(UpdatePricing), request);

                Pricing pricing = _context.Pricings.FirstOrDefault(p => p.Id == request.Id);
                if (pricing == null)
                {
                    _logger.LogMethodStop(LogSource, nameof(UpdatePricing));
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Pricing not found.");
                }

                pricing.HourlyRate = request.Hourly;
                pricing.DailyRate = request.Daily;
                pricing.MonthlyRate = request.Monthly;
                pricing.YearlyRate = request.Yearly;

                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(UpdatePricing));
                return BaseResponse<string>.SuccessResponse("Pricing updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdatePricing), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> DeletePricing(DeletePricingRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(DeletePricing), request);

                Pricing pricing = _context.Pricings.FirstOrDefault(p => p.Id == request.Id);
                if (pricing == null)
                {
                    _logger.LogMethodStop(LogSource, nameof(DeletePricing));
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Pricing not found.");
                }

                _context.Pricings.Remove(pricing);
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(DeletePricing));
                return BaseResponse<string>.SuccessResponse("Pricing deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeletePricing), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<PricingResponse> GetPricingById(GetPricingByIdRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetPricingById), request);

                Pricing pricing = _context.Pricings.FirstOrDefault(p => p.Id == request.Id);
                if (pricing == null)
                {
                    _logger.LogMethodStop(LogSource, nameof(GetPricingById));
                    return BaseResponse<PricingResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Pricing not found.");
                }

                PricingResponse response = new PricingResponse
                {
                    Hourly = pricing.HourlyRate,
                    Daily = pricing.DailyRate,
                    Monthly = pricing.MonthlyRate,
                    Yearly = pricing.YearlyRate
                };

                _logger.LogMethodStop(LogSource, nameof(GetPricingById));
                return BaseResponse<PricingResponse>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetPricingById), ex);
                return BaseResponse<PricingResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<List<PricingResponse>> GetAllPricings()
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetAllPricings));

                List<PricingResponse> pricings = _context.Pricings
                    .Select(p => new PricingResponse
                    {
                        Hourly = p.HourlyRate,
                        Daily = p.DailyRate,
                        Monthly = p.MonthlyRate,
                        Yearly = p.YearlyRate
                    })
                    .ToList();

                _logger.LogMethodStop(LogSource, nameof(GetAllPricings));
                return BaseResponse<List<PricingResponse>>.SuccessResponse(pricings);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllPricings), ex);
                return BaseResponse<List<PricingResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
    }
}
