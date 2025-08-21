using coreworking_space_booking_backend.Dtos.Request.Role;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Role;
using coreworking_space_booking_backend.Services.RoleService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create(RoleRequest request)
        {
            return _roleService.CreateRole(request);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<RoleResponse>> GetAll()
        {
            return _roleService.GetAllRoles();
        }

        [HttpPost("get-by-id")]
        public BaseResponse<RoleResponse> GetById(GetRoleByIdRequest request)
        {
            return _roleService.GetRoleById(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update(UpdateRoleRequest request)
        {
            return _roleService.UpdateRole(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> Delete(DisableRoleRequest request)
        {
            return _roleService.DeleteRole(request);
        }
    }
}
