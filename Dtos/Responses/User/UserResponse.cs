namespace coreworking_space_booking_backend.Dtos.Responses.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
    }
}
