using coreworking_space_booking_backend.Dtos.Requests.Pricing;
using coreworking_space_booking_backend.Dtos.Responses.Pricing;
using coreworking_space_booking_backend.Dtos.Responses;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.PricingService
{
    public interface IPricingService
    {
        BaseResponse<string> CreatePricing(PricingRequest request);
        BaseResponse<string> UpdatePricing(UpdatePricingRequest request);
        BaseResponse<string> DeletePricing(DeletePricingRequest request);
        BaseResponse<PricingResponse> GetPricingById(GetPricingByIdRequest request);
        BaseResponse<List<PricingResponse>> GetAllPricings();
    }
}
