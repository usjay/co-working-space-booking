using coreworking_space_booking_backend.Dtos.Responses.Pricing;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Models;

namespace coreworking_space_booking_backend.Dtos.Responses.Product
{
    public class ProductResponse
    {
        //public int Id { get; set; }
        //public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        //public string Image { get; set; }
        public string OperationHours { get; set; }
        public int Capacity { get; set; }

        public List<string> Images { get; set; }

        public int TotalReviews { get; set; }
        //public List<RatingResponse> Ratings { get; set; } 
        public double AverageRating { get; set; }
        public List<PricingResponse> Pricing { get; set; }

        public string LocationUrl { get; set; }


        public List<string> DefaultFacilities { get; set; }
        public List<string> AdditionalFacilities { get; set; }

        public List<RatingResponse> RecentRatings { get; set; }



    }
}
