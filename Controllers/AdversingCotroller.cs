using coreworking_space_booking_backend.Dtos.Requests.Advertising;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Advertising;
using coreworking_space_booking_backend.Services.AdvertisingService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/advertising")]
    [ApiController]
    public class AdvertisingController : ControllerBase
    {
        private readonly IAdvertisingService _advertisingService;

        public AdvertisingController(IAdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create(CreateAdvertisingRequest request)
        {
            return _advertisingService.CreateAdvertising(request);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<AdvertisingResponse>> GetAll()
        {
            return _advertisingService.GetAllAdvertising();
        }

        [HttpPost("get-by-id")]
        public BaseResponse<AdvertisingResponse> GetById(GetAdvertisingByIdRequest request)
        {
            return _advertisingService.GetAdvertisingById(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update(UpdateAdvertisingRequest request)
        {
            return _advertisingService.UpdateAdvertising(request);
        }

        [HttpPost("disable")]
        public BaseResponse<string> Disable(DisableAdvertisingRequest request)
        {
            return _advertisingService.DisableAdvertising(request);
        }
    }
}
