using coreworking_space_booking_backend.Dtos.Request.Role;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Role;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.RoleService
{
    public interface IRoleService
    {
        BaseResponse<string> CreateRole(RoleRequest request);
        BaseResponse<List<RoleResponse>> GetAllRoles();
        BaseResponse<RoleResponse> GetRoleById(GetRoleByIdRequest request);
        BaseResponse<string> UpdateRole(UpdateRoleRequest request);
        BaseResponse<string> DeleteRole(DisableRoleRequest request);
    }
}
