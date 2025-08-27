using coreworking_space_booking_backend.Dtos.Requests.Product;

using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Product;
using coreworking_space_booking_backend.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromForm] ProductRequest request)
        {
            BaseResponse<string> result = _productService.CreateProduct(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-product-list")]
        public IActionResult GetProductList()
        {
            BaseResponse<List<ProductResponse>> result = _productService.GetAllProducts();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("get-product-by-id")]
        public IActionResult GetProductById([FromBody] GetProductByIdRequest request)
        {
            BaseResponse<ProductResponse> result = _productService.GetProductById(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("update-product")]
        public IActionResult UpdateProduct([FromForm] UpdateProductRequest request)
        {
            BaseResponse<string> result = _productService.UpdateProduct(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("delete-product")]
        public IActionResult DeleteProduct([FromBody] DeleteProductRequest request)
        {
            BaseResponse<string> result = _productService.DeleteProduct(request);
            return StatusCode(result.StatusCode, result);
        }

       

      
        [HttpPost("filter-products")]
        public IActionResult FilterProducts([FromBody] ProductFilterRequest request)
        {
            BaseResponse<List<ProductResponse>> result = _productService.GetProductsByFilter(request);
            return StatusCode(result.StatusCode, result);
        }


        //[HttpPost("sorted")]
        //public ActionResult<BaseResponse<List<ProductResponse>>> GetSortedProducts([FromBody] ProductSortRequest request)
        //{
        //    BaseResponse<List<ProductResponse>> response = _productService.GetSortedProducts(request);
        //    return StatusCode(response.StatusCode, response);
        //}
    }
}
