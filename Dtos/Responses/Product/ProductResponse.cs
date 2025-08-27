using System.Text.Json.Serialization;
using coreworking_space_booking_backend.Dtos.Responses.Pricing;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Models;

namespace coreworking_space_booking_backend.Dtos.Responses.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        //public int CompanyId { get; set; }


        
        public string Name { get; set; }

        public string Discription { get; set; }

        [JsonPropertyName("productType")]
        public string Type { get; set; }
        //public string Image { get; set; }
        public string OperationHours { get; set; }

        //[JsonPropertyName("maxCapacity")]
        public int Capacity { get; set; }

        public string ProductDescription { get; set; }
        public List<string> Images { get; set; }

        [JsonPropertyName("reviews")]
        public int TotalReviews { get; set; }
        //public List<RatingResponse> Ratings { get; set; } 

        [JsonPropertyName("rating")]
        public double AverageRating { get; set; }






        public List<PricingResponse> Pricing { get; set; }

      [JsonPropertyName("location")]
        public string LocationUrl { get; set; }
        public string Address { get; set; }


        [JsonPropertyName("features")]
        public List<string> DefaultFacilities { get; set; }
        public List<string> AdditionalFacilities { get; set; }

        public List<RatingResponse> RecentRatings { get; set; }

        public List<AvailabilityResponse> Availability { get; set; } = new List<AvailabilityResponse>();

        //public List<Availability> Availability { get; set; }


    }
}
