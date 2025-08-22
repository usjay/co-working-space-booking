namespace coreworking_space_booking_backend.Dtos.Requests.Product
{
    public class SearchProductsRequest
    {
        public int? LocationId { get; set; }   
        public string? Type { get; set; }      
    }
}
