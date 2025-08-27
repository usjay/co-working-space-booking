using System.ComponentModel.DataAnnotations;

namespace coreworking_space_booking_backend.Dtos.Requests.Product
{
    public class ProductRequest
    {
       
       
        public int CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public int LocationId { get; set; }
        public string OperationHours { get; set; }
        public int Capacity { get; set; }

        public string Discription { get; set; }




        public List<IFormFile> Images { get; set; }

        
    }

    public class UpdateProductRequest : ProductRequest
    {
        [Required]
        public int Id { get; set; }

        public int LocationId { get; set; }

        public int CreatedAt { get; set; }

        public List<IFormFile> Images { get; set; }
    }

    public class GetProductByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class DeleteProductRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
