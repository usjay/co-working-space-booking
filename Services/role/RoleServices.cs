using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Request.Role;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Role;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private const string LogSource = "RoleService";

        public RoleService(ApplicationDbContext context, IAppLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public BaseResponse<string> CreateRole(RoleRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(CreateRole));

                Role role = new Role
                {
                    Name = request.Name,
                    Description = request.Description,
                    Type = request.Type,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Roles.Add(role);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Role created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(CreateRole), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<List<RoleResponse>> GetAllRoles()
        {
            try
            {
                var roles = _context.Roles
                    .Where(r => r.IsActive)
                    .Select(r => new RoleResponse
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Type = r.Type,
                        IsActive = r.IsActive,
                       
                    })
                    .ToList();

                if (!roles.Any())
                    return BaseResponse<List<RoleResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No roles found");

                return BaseResponse<List<RoleResponse>>.SuccessResponse(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllRoles), ex);
                return BaseResponse<List<RoleResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<RoleResponse> GetRoleById(GetRoleByIdRequest request)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == request.Id && r.IsActive);

                if (role == null)
                    return BaseResponse<RoleResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Role not found");

                var response = new RoleResponse
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    Type = role.Type,
                    IsActive = role.IsActive,
                    
                };

                return BaseResponse<RoleResponse>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetRoleById), ex);
                return BaseResponse<RoleResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> UpdateRole(UpdateRoleRequest request)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == request.Id && r.IsActive);
                if (role == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Role not found");

                role.Name = request.Name;
                role.Description = request.Description;
                role.Type = request.Type;
                role.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
                return BaseResponse<string>.SuccessResponse("Role updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdateRole), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> DeleteRole(DisableRoleRequest request)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == request.Id && r.IsActive);
                if (role == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Role not found");

                role.IsActive = false;
                role.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
                return BaseResponse<string>.SuccessResponse("Role deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeleteRole), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
