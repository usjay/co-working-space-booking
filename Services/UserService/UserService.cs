using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Request.User;
using coreworking_space_booking_backend.Dtos.Requests.User;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.User;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using BCrypt.Net;
using coreworking_space_booking_backend.Helpers.ImageUpload;

namespace coreworking_space_booking_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ImageUploadHelper _imageHelper;
        private const string LogSource = "UserService";

        public UserService(ApplicationDbContext context, IAppLogger logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _imageHelper = new ImageUploadHelper(configuration); 
        }

        private string SaveAvatar(IFormFile file)
        {
            if (file == null) return null;

            return _imageHelper.SaveFile(file, "UserAvatar");
        }

        private UserResponse MapToResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Company = user.Company,
                JobTitle = user.JobTitle,
                Bio = user.Bio,
                Avatar = user.Avatar,
                IsActive = user.IsActive
            };
        }

        public BaseResponse<string> CreateUser(UserRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(CreateUser));

                if (_context.Users.Any(u => u.Email == request.Email && u.IsActive))
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status409Conflict, "User with this email already exists.");

                string avatarPath = request.Avatar != null ? SaveAvatar(request.Avatar) : null;

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Company = request.Company,
                    JobTitle = request.JobTitle,
                    Bio = request.Bio,
                    Avatar = avatarPath,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("User created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(CreateUser), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<List<UserResponse>> GetAllUsers()
        {
            try
            {
                List<User> users = _context.Users.Where(u => u.IsActive).ToList();

                if (!users.Any()) return BaseResponse<List<UserResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No active users found");

                List<UserResponse> response = users.Select(u => MapToResponse(u)).ToList();
                return BaseResponse<List<UserResponse>>.SuccessResponse(response, "Users retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllUsers), ex);
                return BaseResponse<List<UserResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<UserResponse> GetUserById(GetUserByIdRequest request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == request.Id && u.IsActive);
                if (user == null)
                    return BaseResponse<UserResponse>.ErrorResponse(StatusCodes.Status404NotFound, "User not found");

                return BaseResponse<UserResponse>.SuccessResponse(MapToResponse(user), "User retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetUserById), ex);
                return BaseResponse<UserResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == request.Id && u.IsActive);
                if (user == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "User not found");

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.Phone = request.Phone;
                user.Company = request.Company;
                user.JobTitle = request.JobTitle;
                user.Bio = request.Bio;

                if (request.Avatar != null)
                    user.Avatar = SaveAvatar(request.Avatar);

                user.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("User updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdateUser), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> DeleteUser(DisableUserRequest request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == request.Id && u.IsActive);
                if (user == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "User not found");

                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("User disabled successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeleteUser), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<UserLoginResponse> Login(UserLoginRequest request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.IsActive);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    return BaseResponse<UserLoginResponse>.ErrorResponse(StatusCodes.Status401Unauthorized, "Invalid email or password");

                var token = GenerateJwtToken(user);

                var response = new UserLoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Token = token
                };

                return BaseResponse<UserLoginResponse>.SuccessResponse(response, "Login successful");
            }
            catch (Exception ex)
            {
                return BaseResponse<UserLoginResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
