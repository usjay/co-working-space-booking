using coreworking_space_booking_backend.Dtos.Requests.Permission;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Permission;
using coreworking_space_booking_backend.Services.PermissionService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create(PermissionCreateRequestDto request)
        {
            return _permissionService.CreatePermission(request);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<PermissionListResponseDto>> GetAll()
        {
            return _permissionService.GetPermissionList();
        }

        [HttpPost("update")]
        public BaseResponse<string> Update(PermissionUpdateRequestDto request)
        {
            return _permissionService.UpdatePermission(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> Delete(PermissionDeleteRequestDto request)
        {
            return _permissionService.DeletePermission(request);
        }

        [HttpPost("get-by-role")]
        public BaseResponse<List<PermissionDetailsResponseDto>> GetByRole(PermissionDetailsRequestDto request)
        {
            return _permissionService.GetPermissionsByRoleId(request);
        }
    }
}
