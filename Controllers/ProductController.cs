using coreworking_space_booking_backend.Dtos.Requests.Product;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Product;
using coreworking_space_booking_backend.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create([FromForm] ProductRequest request)
        {
            return _productService.CreateProduct(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update([FromForm] UpdateProductRequest request)
        {
            return _productService.UpdateProduct(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> Delete([FromForm] DeleteProductRequest request)
        {
            return _productService.DeleteProduct(request);
        }

        [HttpPost("get-by-id")]
        public BaseResponse<ProductResponse> GetById([FromForm] GetProductByIdRequest request)
        {
            return _productService.GetProductById(request);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<ProductResponse>> GetAll()
        {
            return _productService.GetAllProducts();
        }

        [HttpPost("search")]
        public BaseResponse<List<ProductResponse>> Search([FromBody] SearchProductsRequest request)
        {
            return _productService.SearchProducts(request);
        }
    }
}
