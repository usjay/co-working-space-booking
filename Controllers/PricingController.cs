using coreworking_space_booking_backend.Dtos.Requests.Pricing;
using coreworking_space_booking_backend.Dtos.Responses.Pricing;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Services.PricingService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/pricings")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create([FromBody] PricingRequest request)
        {
            return _pricingService.CreatePricing(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update([FromBody] UpdatePricingRequest request)
        {
            return _pricingService.UpdatePricing(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> Delete([FromBody] DeletePricingRequest request)
        {
            return _pricingService.DeletePricing(request.Id);
        }

        [HttpPost("get-by-id")]
        public BaseResponse<PricingResponse> GetById([FromBody] GetPricingByIdRequest request)
        {
            return _pricingService.GetPricingById(request.Id);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<PricingResponse>> GetAll()
        {
            return _pricingService.GetAllPricings();
        }
    }
}
