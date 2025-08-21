using coreworking_space_booking_backend.Dtos.Request.Admin;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Admin;
using coreworking_space_booking_backend.Services.AdminService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create([FromForm] AdminRequest request)
        {
            return _adminService.CreateAdmin(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update([FromForm] UpdateAdminRequest request)
        {
            return _adminService.UpdateAdmin(request);
        }

        [HttpPost("disable")]
        public BaseResponse<string> Disable([FromBody] DisableAdminRequest request)
        {
            return _adminService.DisableAdmin(request);
        }

        [HttpPost("get-by-id")]
        public BaseResponse<AdminResponse> GetById([FromBody] GetAdminByIdRequest request)
        {
            return _adminService.GetAdminById(request);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<AdminResponse>> GetAll()  
        {
            return _adminService.GetAllAdmins();
        }
    }
}
