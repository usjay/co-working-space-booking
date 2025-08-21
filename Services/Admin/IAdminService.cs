using coreworking_space_booking_backend.Dtos.Request.Admin;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Admin;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.AdminService
{
    public interface IAdminService
    {
        BaseResponse<string> CreateAdmin(AdminRequest request);
        BaseResponse<string> UpdateAdmin(UpdateAdminRequest request);
        BaseResponse<string> DisableAdmin(DisableAdminRequest request);
        BaseResponse<List<AdminResponse>> GetAllAdmins();
        BaseResponse<AdminResponse> GetAdminById(GetAdminByIdRequest request);
    }
}
