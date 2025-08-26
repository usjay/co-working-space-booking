using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.Location
{
    public class LocationRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string Url { get; set; }
    }

    public class UpdateLocationRequest : LocationRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class GetLocationByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class DeleteLocationRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
