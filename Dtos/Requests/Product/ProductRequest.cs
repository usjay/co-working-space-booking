using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace coreworking_space_booking_backend.Dtos.Requests.Product
{
    public class ProductRequest
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public string OperationHours { get; set; }

        public int Capacity { get; set; }

        [Required]
        public int LocationId { get; set; }

        public IFormFile? Image { get; set; }

        public List<string> Images { get; set; }

        public List<string> Features { get; set; }

        public string Availability { get; set; }

        public bool IsActive { get; set; }
    }

    public class UpdateProductRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int LocationId { get; set; }

        public string OperationHours { get; set; }

        public int Capacity { get; set; }

        public IFormFile? Image { get; set; }

        public List<string> Images { get; set; }

        public List<string> Features { get; set; }

        public string Availability { get; set; }

        public bool IsActive { get; set; }
    }

    public class GetProductByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class DeleteProductRequest
    {
        [Required]
        public int ProductId { get; set; }
    }

    public class GetSortedProductsRequest
    {
        public string SortBy { get; set; } = "dailyRate"; 
        public bool Ascending { get; set; } = true;
        public bool FilterByAvailability { get; set; } = false;
    }
}
