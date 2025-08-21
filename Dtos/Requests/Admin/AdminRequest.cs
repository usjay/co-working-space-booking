using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Request.Admin
{
    public class AdminRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string Bio { get; set; }

        [Required]
        public IFormFile Avatar { get; set; }
    }

    public class UpdateAdminRequest : AdminRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class GetAdminByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class DisableAdminRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
