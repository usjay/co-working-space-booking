using coreworking_space_booking_backend.Dtos.Requests.Location;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Location;
using coreworking_space_booking_backend.Services.LocationService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create(LocationRequest request)
        {
            return _locationService.CreateLocation(request);
        }

        //[HttpPost("get-all")]
        //public BaseResponse<List<LocationResponse>> GetAll()
        //{
        //    return _locationService.GetAllLocations();
        //}


        [HttpPost("get-all")]
        public BaseResponse<Dictionary<string, List<string>>> GetAll()
        {
            return _locationService.GetAllLocations();
        }


        [HttpPost("get-by-id")]
        public BaseResponse<LocationResponse> GetById(GetLocationByIdRequest request)
        {
            return _locationService.GetLocationById(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update(UpdateLocationRequest request)
        {
            return _locationService.UpdateLocation(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> Delete(DeleteLocationRequest request)
        {
            return _locationService.DeleteLocation(request);
        }
    }
}
