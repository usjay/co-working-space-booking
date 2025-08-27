using coreworking_space_booking_backend.Dtos.Requests.Product;
using coreworking_space_booking_backend.Dtos.Responses.Product;
using coreworking_space_booking_backend.Dtos.Responses;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.ProductService
{
    public interface IProductService
    {
        BaseResponse<string> CreateProduct(ProductRequest request);
        BaseResponse<string> UpdateProduct(UpdateProductRequest request);
        BaseResponse<string> DeleteProduct(DeleteProductRequest request);
        BaseResponse<List<ProductResponse>> GetAllProducts();
        BaseResponse<ProductResponse> GetProductById(GetProductByIdRequest request);
        BaseResponse<List<ProductResponse>> SearchProducts(SearchProductsRequest request);

        //BaseResponse<List<ProductResponse>> GetProductsSortedAndFiltered(GetSortedProductsRequest request);
    }
}
