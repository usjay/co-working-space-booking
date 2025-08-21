using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Request.Role
{
    public class RoleRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
    }

    public class UpdateRoleRequest : RoleRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class GetRoleByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class DisableRoleRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
