namespace coreworking_space_booking_backend.Dtos.Requests.Permission
{
    public class PermissionUpdateRequestDto
    {
        public long PermissionId { get; set; }
        public string Description { get; set; }
        public long RoleId { get; set; }
    }
}
