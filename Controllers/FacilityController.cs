using coreworking_space_booking_backend.Dtos.Requests.Facility;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Facility;
using coreworking_space_booking_backend.Services.FacilityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;
        public FacilityController(IFacilityService FacilityService)
        {
            _facilityService = FacilityService;
        }

        [HttpPost("create-facility")]
        public IActionResult CreateFacility([FromForm] FacilityCreateRequestDto request)
        {
            BaseResponse<string> result = _facilityService.CreateFacility(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-facility")]
        public IActionResult GetFacilityById([FromBody] FacilityDetailsRequestDto request)
        {
            BaseResponse<FacilityDetailsResponseDto> result = _facilityService.GetFacilityById(request);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("get-facility-list")]
        public IActionResult GetFacilityList()
        {
            BaseResponse<List<FacilityListResponseDto>> result = _facilityService.GetFacilityList();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("update-facility")]
        public IActionResult UpdateFacility([FromForm] FacilityUpdateRequestDto request)
        {
            BaseResponse<string> result = _facilityService.UpdateFacility(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("delete-facility")]
        public IActionResult DeleteFacility([FromBody] FacilityDeleteRequestDto request)
        {
            BaseResponse<string> result = _facilityService.DeleteFacility(request);
            return StatusCode(result.StatusCode, result);
        }

    }
}
