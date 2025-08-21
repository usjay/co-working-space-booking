using coreworking_space_booking_backend.Dtos.Request.User;
using coreworking_space_booking_backend.Dtos.Requests.User;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.User;
using coreworking_space_booking_backend.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create([FromForm] UserRequest request)
        {
            return _userService.CreateUser(request);
        }


        [HttpPost("get-all")]
        public BaseResponse<List<UserResponse>> GetAll()
        {
            return _userService.GetAllUsers();
        }

        [HttpPost("get-by-id")]
        public BaseResponse<UserResponse> GetById([FromForm] GetUserByIdRequest request)
        {
            return _userService.GetUserById(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update([FromForm] UpdateUserRequest request)
        {
            return _userService.UpdateUser(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> Delete([FromForm] DisableUserRequest request)
        {
            return _userService.DeleteUser(request);
        }

        [HttpPost("login")]
        public BaseResponse<UserLoginResponse> Login([FromBody] UserLoginRequest request)
        {
            return _userService.Login(request);
        }
    }
}
