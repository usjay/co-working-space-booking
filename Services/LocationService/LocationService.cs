using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Location;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Location;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private const string LogSource = "LocationService";

        public LocationService(ApplicationDbContext context, IAppLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public BaseResponse<string> CreateLocation(LocationRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(CreateLocation));

                Location location = new Location
                {
                    Name = request.Name,
                    Address = request.Address,
                    LocationUrl = request.Url,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Locations.Add(location);
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(CreateLocation));
                return BaseResponse<string>.SuccessResponse("Location created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(CreateLocation), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<List<LocationResponse>> GetAllLocations()
        {
            try
            {
                List<LocationResponse> locations = _context.Locations
                    .Where(l => l.IsActive)
                    .Select(l => new LocationResponse
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Address = l.Address,
                        Url = l.LocationUrl
                    })
                    .ToList();

                if (locations.Count == 0)
                    return BaseResponse<List<LocationResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No locations found");

                return BaseResponse<List<LocationResponse>>.SuccessResponse(locations, "Locations retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllLocations), ex);
                return BaseResponse<List<LocationResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<LocationResponse> GetLocationById(GetLocationByIdRequest request)
        {
            try
            {
                Location location = _context.Locations.FirstOrDefault(l => l.Id == request.Id && l.IsActive);

                if (location == null)
                    return BaseResponse<LocationResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Location not found");

                LocationResponse response = new LocationResponse
                {
                    Id = location.Id,
                    Name = location.Name,
                    Address = location.Address,
                    Url = location.LocationUrl
                };

                return BaseResponse<LocationResponse>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetLocationById), ex);
                return BaseResponse<LocationResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> UpdateLocation(UpdateLocationRequest request)
        {
            try
            {
                Location location = _context.Locations.FirstOrDefault(l => l.Id == request.Id && l.IsActive);

                if (location == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Location not found");

                location.Name = request.Name;
                location.Address = request.Address;
                location.LocationUrl = request.Url;
                location.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Location updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdateLocation), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> DeleteLocation(DeleteLocationRequest request)
        {
            try
            {
                Location location = _context.Locations.FirstOrDefault(l => l.Id == request.Id && l.IsActive);

                if (location == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Location not found");

                location.IsActive = false;
                location.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Location deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeleteLocation), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
