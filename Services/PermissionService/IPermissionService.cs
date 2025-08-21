using coreworking_space_booking_backend.Dtos.Requests.Facility;
using coreworking_space_booking_backend.Dtos.Requests.Permission;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Facility;
using coreworking_space_booking_backend.Dtos.Responses.Permission;

namespace coreworking_space_booking_backend.Services.PermissionService
{
    public interface IPermissionService
    {
        public BaseResponse<string> CreatePermission(PermissionCreateRequestDto request);
        public BaseResponse<List<PermissionListResponseDto>> GetPermissionList();
        public BaseResponse<string> UpdatePermission(PermissionUpdateRequestDto request);
        public BaseResponse<string> DeletePermission(PermissionDeleteRequestDto request);
        public BaseResponse<List<PermissionDetailsResponseDto>> GetPermissionsByRoleId(PermissionDetailsRequestDto request);

    }
}
