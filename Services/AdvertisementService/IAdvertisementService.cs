using coreworking_space_booking_backend.Dtos.Requests.Advertising;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Advertising;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.AdvertisingService
{
    public interface IAdvertisingService
    {
        BaseResponse<string> CreateAdvertising(CreateAdvertisingRequest request);
        BaseResponse<string> UpdateAdvertising(UpdateAdvertisingRequest request);
        BaseResponse<string> DisableAdvertising(DisableAdvertisingRequest request);
        BaseResponse<AdvertisingResponse> GetAdvertisingById(GetAdvertisingByIdRequest request);
        BaseResponse<List<AdvertisingResponse>> GetAllAdvertising();
    }
}
