using coreworking_space_booking_backend.Dtos.Requests.Location;
using coreworking_space_booking_backend.Dtos.Responses.Location;
using coreworking_space_booking_backend.Dtos.Responses;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.LocationService
{
    public interface ILocationService
    {
        BaseResponse<string> CreateLocation(LocationRequest request);
        //BaseResponse<List<LocationResponse>> GetAllLocations();

        BaseResponse<Dictionary<string, List<string>>> GetAllLocations();

        BaseResponse<LocationResponse> GetLocationById(GetLocationByIdRequest request);
        BaseResponse<string> UpdateLocation(UpdateLocationRequest request);
        BaseResponse<string> DeleteLocation(DeleteLocationRequest request);
    }
}
