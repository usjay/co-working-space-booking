using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Advertising;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Advertising;
using coreworking_space_booking_backend.Helpers.ImageUpload;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.AdvertisingService
{
    public class AdvertisingService : IAdvertisingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private readonly IConfiguration _configuration;
        private readonly ImageUploadHelper _imageHelper;
        private readonly string _logSource;

        public AdvertisingService(ApplicationDbContext context, IAppLogger appLogger, IConfiguration configuration, ImageUploadHelper imageHelper)
        {
            _context = context;
            _appLogger = appLogger;
            _configuration = configuration;
            _imageHelper = imageHelper;
            _logSource = GetType().Name;
        }

        private string SaveImage(IFormFile image)
        {
            if (image == null) return null;
            return _imageHelper.SaveFile(image, "AdvertisingImages");
        }

        public BaseResponse<string> CreateAdvertising(CreateAdvertisingRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CreateAdvertising), request);

                string imagePath = SaveImage(request.Image);

                Advertising ad = new Advertising
                {
                    CompanyName = request.CompanyName,
                    Description = request.Description,
                    ImagePath = imagePath,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Advertisings.Add(ad);
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(CreateAdvertising));
                return BaseResponse<string>.SuccessResponse("Advertising created successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CreateAdvertising), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> UpdateAdvertising(UpdateAdvertisingRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(UpdateAdvertising), request);

                Advertising ad = _context.Advertisings.FirstOrDefault(a => a.Id == request.Id && a.IsActive);
                if (ad == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Advertising not found.");

                if (request.Image != null)
                    ad.ImagePath = SaveImage(request.Image);

                ad.CompanyName = request.CompanyName;
                ad.Description = request.Description;
                ad.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(UpdateAdvertising));
                return BaseResponse<string>.SuccessResponse("Advertising updated successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(UpdateAdvertising), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> DisableAdvertising(DisableAdvertisingRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(DisableAdvertising), request);

                Advertising ad = _context.Advertisings.FirstOrDefault(a => a.Id == request.Id);
                if (ad == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Advertising not found.");

                if (!ad.IsActive)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status400BadRequest, "Advertising already disabled.");

                ad.IsActive = false;
                ad.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(DisableAdvertising));
                return BaseResponse<string>.SuccessResponse("Advertising disabled successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(DisableAdvertising), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<AdvertisingResponse> GetAdvertisingById(GetAdvertisingByIdRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetAdvertisingById), request);

                Advertising ad = _context.Advertisings.FirstOrDefault(a => a.Id == request.Id && a.IsActive);
                if (ad == null)
                    return BaseResponse<AdvertisingResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Advertising not found.");

                AdvertisingResponse response = new AdvertisingResponse
                {
                    CompanyName = ad.CompanyName,
                    Description = ad.Description,
                    ImagePath = ad.ImagePath
                };

                return BaseResponse<AdvertisingResponse>.SuccessResponse(response, "Advertising retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetAdvertisingById), ex);
                return BaseResponse<AdvertisingResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<List<AdvertisingResponse>> GetAllAdvertising()
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetAllAdvertising));

                List<Advertising> ads = _context.Advertisings.Where(a => a.IsActive).ToList();

                if (ads.Count == 0)
                    return BaseResponse<List<AdvertisingResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No active advertising found");

                List<AdvertisingResponse> response = new List<AdvertisingResponse>();
                foreach (Advertising a in ads)
                {
                    AdvertisingResponse adResponse = new AdvertisingResponse
                    {
                        CompanyName = a.CompanyName,
                        Description = a.Description,
                        ImagePath = a.ImagePath
                    };
                    response.Add(adResponse);
                }

                _appLogger.LogMethodStop(_logSource, nameof(GetAllAdvertising));
                return BaseResponse<List<AdvertisingResponse>>.SuccessResponse(response, "Advertising retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetAllAdvertising), ex);
                return BaseResponse<List<AdvertisingResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
    }
}
