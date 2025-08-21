using coreworking_space_booking_backend.Dtos.Requests.Facility;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Facility;

namespace coreworking_space_booking_backend.Services.FacilityService
{
    public interface IFacilityService
    {
        public BaseResponse<string> CreateFacility(FacilityCreateRequestDto request);
        public BaseResponse<FacilityDetailsResponseDto> GetFacilityById(FacilityDetailsRequestDto request);
        public BaseResponse<List<FacilityListResponseDto>> GetFacilityList();
        public BaseResponse<string> UpdateFacility(FacilityUpdateRequestDto request);
        public BaseResponse<string> DeleteFacility(FacilityDeleteRequestDto request);
    }
}
