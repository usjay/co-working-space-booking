namespace coreworking_space_booking_backend.Dtos.Responses.User
{
    public class UserLoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
    }
}
