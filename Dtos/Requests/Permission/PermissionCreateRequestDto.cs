using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Dtos.Requests.Permission
{
    public class PermissionCreateRequestDto
    {
        public required long PermissionId { get; set; }

        public required string Description { get; set; }

        public long RoleId { get; set; }
    }
}
