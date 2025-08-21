using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Facility;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Facility;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models.Facility;

namespace coreworking_space_booking_backend.Services.FacilityService
{
    public class FacilityService : IFacilityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private readonly string _logSource;

        public FacilityService(ApplicationDbContext context, IAppLogger appLogger)
        {
            _context = context;
            _appLogger = appLogger;
            _logSource = GetType().Name;
        }
        public BaseResponse<string> CreateFacility(FacilityCreateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CreateFacility), request);
                
                Facility facility = new Facility
                {
                    FacilityId = request.FacilityId,
                    FacilityName = request.FacilityName,
                    Description = request.Description,
                    FacilityCode = request.FacilityCode,
                    ProductId = request.ProductId,
                    CompanyId = request.CompanyId,
                    LocationId = request.LocationId,
                    IsDefault = request.IsDefault,
                    IsDeleted = false
                };

                _context.Facilities.Add(facility);
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(CreateFacility));              
                return BaseResponse<string>.CreateSuccessResponse();

            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CreateFacility), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }


        public BaseResponse<FacilityDetailsResponseDto> GetFacilityById(FacilityDetailsRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetFacilityById), request);

                var facility = _context.Facilities.FirstOrDefault(f => f.FacilityId == request.FacilityId && !f.IsDeleted);

                if (facility == null)
                {
                    return BaseResponse<FacilityDetailsResponseDto>.ErrorResponse(StatusCodes.Status404NotFound, "Facilities not found or has been deleted");
                }

                var responseDto = new FacilityDetailsResponseDto
                {
                    FacilityId = facility.FacilityId,
                    FacilityName = facility.FacilityName,
                    Description = facility.Description,
                    FacilityCode = facility.FacilityCode,
                    ProductId = facility.ProductId,
                    CompanyId = facility.CompanyId,
                    LocationId = facility.LocationId,
                    IsDefault = facility.IsDefault
                };

                _appLogger.LogMethodStop(_logSource, nameof(GetFacilityById));
                return BaseResponse<FacilityDetailsResponseDto>.SuccessResponse(responseDto, "Facilities retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetFacilityById), ex);
                return BaseResponse<FacilityDetailsResponseDto>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal error, refer to the internal server logs for more information");
            }
        }

        public BaseResponse<List<FacilityListResponseDto>>GetFacilityList()
        {
            try
            {
                var facilities = _context.Facilities
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FacilityListResponseDto
                    {
                        facilityId = f.FacilityId,
                        facilityName = f.FacilityName
                    }).ToList();

                return BaseResponse<List<FacilityListResponseDto>>.SuccessResponse(facilities, "Facilities retrieved successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<List<FacilityListResponseDto>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Failed to retrieve facilities");
            }
        }

        public BaseResponse<string> UpdateFacility(FacilityUpdateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(UpdateFacility), request);

                var facility = _context.Facilities.FirstOrDefault(f => f.FacilityId == request.FacilityId && !f.IsDeleted);
                if (facility == null)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Facilities not found or has been deleted");
                }

                facility.FacilityName = request.FacilityName;
                facility.Description = request.Description;
                facility.FacilityCode = request.FacilityCode;
                facility.ProductId = request.ProductId;
                facility.CompanyId = request.CompanyId;
                facility.LocationId = request.LocationId;
                facility.IsDefault = request.IsDefault;

                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(UpdateFacility));
                return BaseResponse<string>.SuccessResponse("Facilities updated successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(UpdateFacility), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        public BaseResponse<string> DeleteFacility(FacilityDeleteRequestDto request)
{
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(DeleteFacility), request);

                var facility = _context.Facilities.FirstOrDefault(f => f.FacilityId == request.FacilityId);
                if (facility == null)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Facilities not found");
                }

                facility.IsDeleted = true; 
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(DeleteFacility));
                return BaseResponse<string>.SuccessResponse("Facilities deleted successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(DeleteFacility), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }


    }
}
