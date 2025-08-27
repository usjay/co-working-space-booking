using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Request.Admin;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using coreworking_space_booking_backend.Helpers.ImageUpload;
using coreworking_space_booking_backend.Dtos.Responses.Admin;

namespace coreworking_space_booking_backend.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private readonly IConfiguration _configuration;
        private readonly ImageUploadHelper _imageHelper;
        private readonly string _logSource;

        public AdminService(ApplicationDbContext context, IAppLogger appLogger, IConfiguration configuration, ImageUploadHelper imageHelper)
        {
            _context = context;
            _appLogger = appLogger;
            _configuration = configuration;
            _imageHelper = imageHelper;
            _logSource = GetType().Name;
        }

        private string SaveAvatar(IFormFile avatar)
        {
            if (avatar == null) return null;
            return _imageHelper.SaveFile(avatar, "AdminAvatar");
        }

        public BaseResponse<string> CreateAdmin(AdminRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CreateAdmin), request);

                Admin existingAdmin = _context.Admins.FirstOrDefault(a => a.Email == request.Email);
                if (existingAdmin != null)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status409Conflict, "Admin with this email already exists.");
                }

                string avatarPath = SaveAvatar(request.Avatar);

                Admin admin = new Admin
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Company = request.Company,
                    JobTitle = request.JobTitle,
                    Bio = request.Bio,
                    Avatar = avatarPath,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Admins.Add(admin);
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(CreateAdmin));
                return BaseResponse<string>.SuccessResponse("Admin created successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CreateAdmin), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> UpdateAdmin(UpdateAdminRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(UpdateAdmin), request);

                Admin admin = _context.Admins.FirstOrDefault(a => a.Id == request.Id && a.IsActive);
                if (admin == null)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Admin not found.");
                }

                if (request.Avatar != null)
                {
                    admin.Avatar = SaveAvatar(request.Avatar);
                }

                admin.FirstName = request.FirstName;
                admin.LastName = request.LastName;
                admin.Email = request.Email;
                admin.Phone = request.Phone;
                admin.Company = request.Company;
                admin.JobTitle = request.JobTitle;
                admin.Bio = request.Bio;
                admin.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(UpdateAdmin));
                return BaseResponse<string>.SuccessResponse("Admin updated successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(UpdateAdmin), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<string> DisableAdmin(DisableAdminRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(DisableAdmin), request);

                Admin admin = _context.Admins.FirstOrDefault(a => a.Id == request.Id);
                if (admin == null)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Admin not found.");
                }

                if (!admin.IsActive)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status400BadRequest, "Admin already disabled.");
                }

                admin.IsActive = false;
                admin.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(DisableAdmin));
                return BaseResponse<string>.SuccessResponse("Admin disabled successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(DisableAdmin), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<AdminResponse> GetAdminById(GetAdminByIdRequest request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetAdminById), request);

                Admin admin = _context.Admins.FirstOrDefault(a => a.Id == request.Id && a.IsActive);
                if (admin == null)
                {
                    return BaseResponse<AdminResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Admin not found.");
                }

                AdminResponse response = new AdminResponse
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.Email,
                    Phone = admin.Phone,
                    Company = admin.Company,
                    JobTitle = admin.JobTitle,
                    Bio = admin.Bio,
                    Avatar = admin.Avatar,
                    IsActive = admin.IsActive
                };

                return BaseResponse<AdminResponse>.SuccessResponse(response, "Admin retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetAdminById), ex);
                return BaseResponse<AdminResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        public BaseResponse<List<AdminResponse>> GetAllAdmins()
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetAllAdmins));

                List<Admin> admins = _context.Admins.Where(a => a.IsActive).ToList();

                if (admins.Count == 0)
                    return BaseResponse<List<AdminResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No active admins found");

                List<AdminResponse> response = new List<AdminResponse>();
                foreach (Admin a in admins)
                {
                    AdminResponse adminResponse = new AdminResponse
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email,
                        Phone = a.Phone,
                        Company = a.Company,
                        JobTitle = a.JobTitle,
                        Bio = a.Bio,
                        Avatar = a.Avatar,
                        IsActive = a.IsActive
                    };
                    response.Add(adminResponse);
                }

                _appLogger.LogMethodStop(_logSource, nameof(GetAllAdmins));
                return BaseResponse<List<AdminResponse>>.SuccessResponse(response, "Admins retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetAllAdmins), ex);
                return BaseResponse<List<AdminResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
    }
}
