using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Request.User
{
    public class UserRequest
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

        public string Password { get; set; }


        [Required]
        public IFormFile Avatar { get; set; }
    }


    public class UpdateUserRequest : UserRequest
    {
        [Required]
        public int Id { get; set; }
    }

   
    public class GetUserByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

  
    public class DisableUserRequest
    {
        [Required]
        public int Id { get; set; }
    }

   
       
    }












