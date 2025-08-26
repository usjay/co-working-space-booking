namespace coreworking_space_booking_backend.Dtos.Requests.Product
{
    public class SearchProductsRequest
    {
        public int? LocationId { get; set; }     
        public string? Type { get; set; }       
        public int? Capacity { get; set; }       
        public decimal? MinPrice { get; set; }   
        public decimal? MaxPrice { get; set; }  
        public int Page { get; set; } = 1;      
        public int Limit { get; set; } = 10;   
    }
}
