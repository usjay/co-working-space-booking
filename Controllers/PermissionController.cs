using coreworking_space_booking_backend.Dtos.Requests.Permission;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Permission;
using coreworking_space_booking_backend.Services.PermissionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService PermissionService)
        {
            _permissionService = PermissionService;
        }

        [HttpPost("create-permission")]
        public IActionResult CreatePermission([FromBody] PermissionCreateRequestDto request)
        {
            BaseResponse<string> result = _permissionService.CreatePermission(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-permission-list")]
        public IActionResult GetPermissionList()
        {
            BaseResponse<List<PermissionListResponseDto>> result = _permissionService.GetPermissionList();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("update-permission")]
        public IActionResult UpdatePermission([FromBody] PermissionUpdateRequestDto request)
        {
            BaseResponse<string> result = _permissionService.UpdatePermission(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("delete-permission")]
        public IActionResult DeletePermission([FromBody] PermissionDeleteRequestDto request)
        {
            BaseResponse<string> result = _permissionService.DeletePermission(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-permissions-by-role")]
        public IActionResult GetPermissionsByRoleId([FromBody] PermissionDetailsRequestDto request)
        {
            BaseResponse<List<PermissionDetailsResponseDto>> result = _permissionService.GetPermissionsByRoleId(request);
            return StatusCode(result.StatusCode, result);
        }

    }
}
