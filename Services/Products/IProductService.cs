using coreworking_space_booking_backend.Dtos.Requests.Product;

using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Product;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.ProductService
{
    public interface IProductService
    {
        BaseResponse<string> CreateProduct(ProductRequest request);
        BaseResponse<List<ProductResponse>> GetAllProducts();
        BaseResponse<ProductResponse> GetProductById(GetProductByIdRequest request);
        BaseResponse<string> UpdateProduct(UpdateProductRequest request);
        BaseResponse<string> DeleteProduct(DeleteProductRequest request);

       

        BaseResponse<List<ProductResponse>> GetProductsByFilter(ProductFilterRequest request);
    }
}
