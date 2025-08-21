using coreworking_space_booking_backend.Dtos.Request.User;
using coreworking_space_booking_backend.Dtos.Requests.User;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.User;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.UserService
{
    public interface IUserService
    {
        BaseResponse<string> CreateUser(UserRequest request);
        BaseResponse<string> UpdateUser(UpdateUserRequest request);
        BaseResponse<string> DeleteUser(DisableUserRequest request);
        BaseResponse<UserResponse> GetUserById(GetUserByIdRequest request);
        BaseResponse<UserLoginResponse> Login(UserLoginRequest request);
        BaseResponse<List<UserResponse>> GetAllUsers();
    }
}
