using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Product;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using coreworking_space_booking_backend.Helpers.ImageUpload;

namespace coreworking_space_booking_backend.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ImageUploadHelper _imageHelper;
        private const string LogSource = "ProductService";

        public ProductService(ApplicationDbContext context, IAppLogger logger, IConfiguration configuration, ImageUploadHelper imageHelper)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _imageHelper = imageHelper;
        }

       
        private string SaveImage(IFormFile file)
        {
            if (file == null) return null;
            return _imageHelper.SaveFile(file, "ProductImage");
        }

        public BaseResponse<string> CreateProduct(ProductRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(CreateProduct));

                string imagePath = null;
                if (request.Image != null)
                {
                    imagePath = SaveImage(request.Image);
                }

                Product product = new Product
                {
                    CompanyId = request.CompanyId,
                    Name = request.Name,
                    Type = request.Type,
                    OperationHours = request.OperationHours,
                    Capacity = request.Capacity,
                    Image = imagePath,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse(imagePath, "Product created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(CreateProduct), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<List<ProductRequest>> GetAllProducts()
        {
            try
            {
                var products = _context.Products
                    .Where(p => p.IsActive)
                    .Select(p => new ProductRequest
                    {
                        Id = p.ProductId,
                        CompanyId = p.CompanyId,
                        Name = p.Name,
                        Type = p.Type,
                        OperationHours = p.OperationHours,
                        Capacity = p.Capacity,
                        ImagePath = p.Image
                    })
                    .ToList();

                if (!products.Any())
                {
                    return BaseResponse<List<ProductRequest>>.ErrorResponse(StatusCodes.Status404NotFound, "No products found");
                }

                return BaseResponse<List<ProductRequest>>.SuccessResponse(products, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllProducts), ex);
                return BaseResponse<List<ProductRequest>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> GetProductById(GetProductByIdRequest request)
        {
            try
            {
                Product product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                return BaseResponse<string>.SuccessResponse(product.Image, "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetProductById), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> UpdateProduct(UpdateProductRequest request)
        {
            try
            {
                Product product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                product.CompanyId = request.CompanyId;
                product.Name = request.Name;
                product.Type = request.Type;
                product.OperationHours = request.OperationHours;
                product.Capacity = request.Capacity;

                if (request.Image != null)
                {
                    string imagePath = SaveImage(request.Image);
                    product.Image = imagePath;
                }

                product.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Product updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdateProduct), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        public BaseResponse<string> DeleteProduct(DeleteProductRequest request)
        {
            try
            {
                Product product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                product.IsActive = false;
                product.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Product deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeleteProduct), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
