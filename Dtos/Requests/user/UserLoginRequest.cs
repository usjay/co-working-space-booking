using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.User
{
    public class UserLoginRequest
    {

      
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }

    }

