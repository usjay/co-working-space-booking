using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Product;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Product;
using coreworking_space_booking_backend.Dtos.Responses.Pricing;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Helpers.ImageUpload;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

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



        private ProductResponse MapToResponse(Product product)
        {
            // Fetch ratings for the product
            List<Rating> ratingsFromDb = _context.Ratings
                .Where(r => r.ProductId == product.ProductId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt) // Most recent first
                .ToList();

            double averageRating = ratingsFromDb.Any() ? Math.Round(ratingsFromDb.Average(r => r.Value), 2) : 0;
            int totalReviews = ratingsFromDb.Count;

           
            List<RatingResponse> recentRatings = ratingsFromDb
                .Take(5)
                .Select(r => new RatingResponse
                {
                    //Id = r.Id,
                    Value = r.Value,
                    ReviewDescription = r.ReviewDescription,
                    //CreatedAt = r.CreatedAt
                })
                .ToList();

            List<PricingResponse> pricingList = _context.Pricings
                .Where(pr => pr.ProductId == product.ProductId)
                .Select(pr => new PricingResponse
                {
                    Hourly = pr.HourlyRate,
                    Daily = pr.DailyRate,
                    Monthly = pr.MonthlyRate,
                    Yearly = pr.YearlyRate
                })
                .ToList();

            List<string> defaultFacilities = _context.Facilities
                .Where(f => f.ProductId == product.ProductId && !f.IsDeleted && f.IsDefault)
                .Select(f => f.FacilityName)
                .ToList();

            List<string> additionalFacilities = _context.Facilities
                .Where(f => f.ProductId == product.ProductId && !f.IsDeleted && !f.IsDefault)
                .Select(f => f.FacilityName)
                .ToList();

            string locationUrl = _context.Locations
                .Where(l => l.Id == product.LocationId)
                .Select(l => l.LocationUrl)
                .FirstOrDefault() ?? string.Empty;

            return new ProductResponse
            {
                Name = product.Name ?? string.Empty,
                Type = product.Type ?? string.Empty,
                OperationHours = product.OperationHours ?? string.Empty,
                Capacity = product.Capacity,
                AverageRating = averageRating,
                TotalReviews = totalReviews,
                Pricing = pricingList,
                DefaultFacilities = defaultFacilities,
                AdditionalFacilities = additionalFacilities,
                LocationUrl = locationUrl,
                RecentRatings = recentRatings
            };
        }


        public BaseResponse<string> CreateProduct(ProductRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(CreateProduct), request);

                string imagePath = request.Image != null ? SaveImage(request.Image) : null;

                Product product = new Product
                {
                    CompanyId = request.CompanyId,
                    Name = request.Name,
                    Type = request.Type,
                    OperationHours = request.OperationHours,
                    Capacity = request.Capacity,
                    Image = imagePath,
                    LocationId = request.LocationId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(CreateProduct));
                return BaseResponse<string>.SuccessResponse("Product created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(CreateProduct), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<List<ProductResponse>> GetAllProducts()
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetAllProducts));

                List<Product> productsFromDb = _context.Products
                    .Where(p => p.IsActive)
                    .ToList();

                List<ProductResponse> products = productsFromDb.Select(MapToResponse).ToList();

                if (!products.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No products found");

                _logger.LogMethodStop(LogSource, nameof(GetAllProducts));
                return BaseResponse<List<ProductResponse>>.SuccessResponse(products, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllProducts), ex);
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<ProductResponse> GetProductById(GetProductByIdRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetProductById), request);

                Product product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<ProductResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                _logger.LogMethodStop(LogSource, nameof(GetProductById));
                return BaseResponse<ProductResponse>.SuccessResponse(MapToResponse(product), "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetProductById), ex);
                return BaseResponse<ProductResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<string> UpdateProduct(UpdateProductRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(UpdateProduct), request);

                Product product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                product.CompanyId = request.CompanyId;
                product.Name = request.Name;
                product.Type = request.Type;
                product.OperationHours = request.OperationHours;
                product.Capacity = request.Capacity;
                product.LocationId = request.LocationId;

                if (request.Image != null)
                    product.Image = SaveImage(request.Image);

                product.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(UpdateProduct));
                return BaseResponse<string>.SuccessResponse("Product updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdateProduct), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<string> DeleteProduct(DeleteProductRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(DeleteProduct), request);

                Product product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                product.IsActive = false;
                product.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(DeleteProduct));
                return BaseResponse<string>.SuccessResponse("Product deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeleteProduct), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<List<ProductResponse>> GetProductsByBasicFilter(BasicFilterRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetProductsByBasicFilter), request);

                IQueryable<Product> query = _context.Products.Where(p => p.IsActive);

                if (!string.IsNullOrWhiteSpace(request.Type))
                    query = query.Where(p => p.Type.Contains(request.Type));

                if (request.LocationId.HasValue)
                    query = query.Where(p => p.LocationId == request.LocationId.Value);

                List<ProductResponse> products = query.ToList().Select(MapToResponse).ToList();

                if (!products.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No products found");

                _logger.LogMethodStop(LogSource, nameof(GetProductsByBasicFilter));
                return BaseResponse<List<ProductResponse>>.SuccessResponse(products, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetProductsByBasicFilter), ex);
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<List<ProductResponse>> GetProductsByAdvancedFilter(AdvancedFilterRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetProductsByAdvancedFilter), request);

                IQueryable<Product> query = _context.Products.Where(p => p.IsActive);

                if (!string.IsNullOrWhiteSpace(request.Type))
                    query = query.Where(p => p.Type.Contains(request.Type));

                if (!string.IsNullOrWhiteSpace(request.StartTime))
                    query = query.Where(p => p.OperationHours.StartsWith(request.StartTime));

                if (!string.IsNullOrWhiteSpace(request.EndTime))
                    query = query.Where(p => p.OperationHours.EndsWith(request.EndTime));

                if (request.Date.HasValue)
                    query = query.Where(p => p.CreatedAt.Date == request.Date.Value.Date);

                if (request.Capacity.HasValue)
                    query = query.Where(p => p.Capacity >= request.Capacity.Value);

                List<ProductResponse> products = query.ToList().Select(MapToResponse).ToList();

                if (!products.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No products found");

                _logger.LogMethodStop(LogSource, nameof(GetProductsByAdvancedFilter));
                return BaseResponse<List<ProductResponse>>.SuccessResponse(products, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetProductsByAdvancedFilter), ex);
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<List<ProductResponse>> GetMeetingRooms()
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetMeetingRooms));

                List<Product> productsFromDb = _context.Products
                    .Where(p => p.IsActive && p.Type == "MeetingRooms")
                    .ToList();

                List<ProductResponse> products = productsFromDb.Select(MapToResponse).ToList();

                if (!products.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No meeting rooms found");

                _logger.LogMethodStop(LogSource, nameof(GetMeetingRooms));
                return BaseResponse<List<ProductResponse>>.SuccessResponse(products, "Meeting rooms retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetMeetingRooms), ex);
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<List<ProductResponse>> GetDedicatedDesks()
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetDedicatedDesks));

                List<Product> productsFromDb = _context.Products
                    .Where(p => p.IsActive && p.Type == "DedicatedDesks")
                    .ToList();

                List<ProductResponse> products = productsFromDb.Select(MapToResponse).ToList();

                if (!products.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No dedicated desks found");

                _logger.LogMethodStop(LogSource, nameof(GetDedicatedDesks));
                return BaseResponse<List<ProductResponse>>.SuccessResponse(products, "Dedicated desks retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetDedicatedDesks), ex);
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public BaseResponse<List<ProductResponse>> GetHotDesks()
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetHotDesks));

                List<Product> productsFromDb = _context.Products
                    .Where(p => p.IsActive && p.Type == "HotDesks")
                    .ToList();

                List<ProductResponse> products = productsFromDb.Select(MapToResponse).ToList();

                if (!products.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No hot desks found");

                _logger.LogMethodStop(LogSource, nameof(GetHotDesks));
                return BaseResponse<List<ProductResponse>>.SuccessResponse(products, "Hot desks retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetHotDesks), ex);
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
