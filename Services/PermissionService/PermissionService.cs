using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Permission;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Permission;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models.Permission;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private readonly string _logSource;

        public PermissionService(ApplicationDbContext context, IAppLogger appLogger)
        {
            _context = context;
            _appLogger = appLogger;
            _logSource = GetType().Name;
        }

        public BaseResponse<string> CreatePermission(PermissionCreateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CreatePermission), request);

                Permission permission = new Permission
                {
                    PermissionId = request.PermissionId,
                    Description = request.Description,
                    RoleId = request.RoleId,
                    IsDeleted = false
                };

                _context.Permissions.Add(permission);
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(CreatePermission));
                return BaseResponse<string>.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CreatePermission), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        public BaseResponse<List<PermissionListResponseDto>> GetPermissionList()
        {
            try
            {
                List<PermissionListResponseDto> permissions = _context.Permissions
                    .Where(p => !p.IsDeleted)
                    .Select(p => new PermissionListResponseDto
                    {
                        PermissionId = p.PermissionId,
                        Description = p.Description
                    })
                    .ToList();

                return BaseResponse<List<PermissionListResponseDto>>.SuccessResponse(permissions, "Permissions retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetPermissionList), ex);
                return BaseResponse<List<PermissionListResponseDto>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Failed to retrieve permissions");
            }
        }

        public BaseResponse<string> UpdatePermission(PermissionUpdateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(UpdatePermission), request);

                Permission permission = _context.Permissions.FirstOrDefault(p => p.PermissionId == request.PermissionId && !p.IsDeleted);
                if (permission == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Permission not found or has been deleted");

                permission.Description = request.Description;
                permission.RoleId = request.RoleId;

                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(UpdatePermission));
                return BaseResponse<string>.SuccessResponse("Permission updated successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(UpdatePermission), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        public BaseResponse<string> DeletePermission(PermissionDeleteRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(DeletePermission), request);

                Permission permission = _context.Permissions.FirstOrDefault(p => p.PermissionId == request.PermissionId);
                if (permission == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Permission not found");

                permission.IsDeleted = true;
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(DeletePermission));
                return BaseResponse<string>.SuccessResponse("Permission deleted successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(DeletePermission), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        public BaseResponse<List<PermissionDetailsResponseDto>> GetPermissionsByRoleId(PermissionDetailsRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetPermissionsByRoleId), request);

                List<PermissionDetailsResponseDto> permissions = _context.Permissions
                    .Where(p => p.RoleId == request.RoleId && !p.IsDeleted)
                    .Select(p => new PermissionDetailsResponseDto
                    {
                        PermissionId = p.PermissionId,
                        Description = p.Description
                    })
                    .ToList();

                if (permissions.Count == 0)
                    return BaseResponse<List<PermissionDetailsResponseDto>>.ErrorResponse(StatusCodes.Status404NotFound, "No permissions found for this role");

                _appLogger.LogMethodStop(_logSource, nameof(GetPermissionsByRoleId));
                return BaseResponse<List<PermissionDetailsResponseDto>>.SuccessResponse(permissions, "Permissions retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetPermissionsByRoleId), ex);
                return BaseResponse<List<PermissionDetailsResponseDto>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal error, refer to internal server logs");
            }
        }
    }
}
